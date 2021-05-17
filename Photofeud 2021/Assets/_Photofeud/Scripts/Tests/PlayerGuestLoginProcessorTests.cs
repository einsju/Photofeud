using Moq;
using NUnit.Framework;
using Photofeud.Authentication;
using Photofeud.Profile;

public class PlayerGuestLoginProcessorTests
{
    readonly PlayerLoginGuestProcessor _processor;
    readonly Mock<IPlayerLoginGuestService> _playerLoginService;
    readonly Player _player;

    public PlayerGuestLoginProcessorTests()
    {
        _playerLoginService = new Mock<IPlayerLoginGuestService>();
        _processor = new PlayerLoginGuestProcessor(_playerLoginService.Object);
        _player = new Player("Wktb8xUwmyZCtqUF7qvAGXeWPCt2", "Guest", "player@guest.com");
    }

    [Test]
    public void Should_Raise_Expected_Event_On_Success()
    {
        var authenticationResult = new AuthenticationResult();

        _playerLoginService.Setup(x => x.Login())
            .Returns(async () => authenticationResult = new AuthenticationResult { Code = AuthenticationResultCode.Success });

        var raised = false;

        _processor.PlayerAuthenticated += (sender, args) =>
        {
            raised = true;
        };

        _processor.LoginPlayerAsGuest();

        Assert.IsTrue(raised);
    }

    [Test]
    public void Should_Raise_Expected_Event_On_Error()
    {
        var authenticationResult = new AuthenticationResult();

        _playerLoginService.Setup(x => x.Login())
            .Returns(async () => authenticationResult = new AuthenticationResult { Code = AuthenticationResultCode.Error });

        var raised = false;

        _processor.PlayerAuthenticationFailed += (sender, args) =>
        {
            raised = true;
        };

        _processor.LoginPlayerAsGuest();

        Assert.IsTrue(raised);
    }

    [Test]
    public void Should_Return_Player_On_Success()
    {
        var authenticationResult = new AuthenticationResult();

        _playerLoginService.Setup(x => x.Login())
            .Returns(async () => authenticationResult = new AuthenticationResult { Code = AuthenticationResultCode.Success, Player = _player });

        _processor.LoginPlayerAsGuest();

        Assert.AreSame(authenticationResult.Player, _player);
        Assert.IsNotNull(authenticationResult.Player);
    }

    [Test]
    public void Should_Not_Return_Player_On_Error()
    {
        var authenticationResult = new AuthenticationResult();

        _playerLoginService.Setup(x => x.Login())
            .Returns(async () => authenticationResult = new AuthenticationResult { Code = AuthenticationResultCode.Error });

        _processor.LoginPlayerAsGuest();

        Assert.AreNotSame(authenticationResult.Player, _player);
        Assert.IsNull(authenticationResult.Player);
    }
}
