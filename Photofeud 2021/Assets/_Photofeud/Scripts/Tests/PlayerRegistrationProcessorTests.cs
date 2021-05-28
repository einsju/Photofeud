using Moq;
using NUnit.Framework;
using Photofeud.Abstractions.Authentication;
using Photofeud.Authentication;
using Photofeud.Profile;
using System;

public class PlayerRegistrationProcessorTests
{
    readonly string _displayName;
    readonly string _email;
    readonly string _password;

    readonly PlayerRegistrationProcessor _processor;
    readonly Mock<IPlayerRegistrationService> _playerRegistrationService;
    readonly Player _player;

    public PlayerRegistrationProcessorTests()
    {
        _displayName = "7r78";
        _email = "email@email.com";
        _password = "password";

        _playerRegistrationService = new Mock<IPlayerRegistrationService>();
        _processor = new PlayerRegistrationProcessor(_playerRegistrationService.Object);
        _player = new Player("Wktb8xUwmyZCtqUF7qvAGXeWPCt2", _displayName, _email);
    }

    [Test]
    public void Should_Throw_Exception_When_DisplayName_Is_Not_Assigned()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => _processor.RegisterPlayer(null, _email, _password));

        Assert.AreEqual("displayName", exception.ParamName);
    }

    [Test]
    public void Should_Throw_Exception_When_Email_Is_Not_Assigned()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => _processor.RegisterPlayer(_displayName, null, _password));

        Assert.AreEqual("email", exception.ParamName);
    }

    [Test]
    public void Should_Throw_Exception_When_Password_Is_Not_Assigned()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => _processor.RegisterPlayer(_displayName, _email, null));

        Assert.AreEqual("password", exception.ParamName);
    }

    [Test]
    public void Should_Raise_Expected_Event_On_Success()
    {
        var authenticationResult = new AuthenticationResult();

        _playerRegistrationService.Setup(x => x.Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns<string, string, string>(async (x, y, z) => authenticationResult = new AuthenticationResult { Code = AuthenticationResultCode.Success });

        var raised = false;

        _processor.PlayerAuthenticated += (sender, args) =>
        {
            raised = true;
        };

        _processor.RegisterPlayer(_displayName, _email, _password);

        Assert.IsTrue(raised);
    }

    [Test]
    public void Should_Raise_Expected_Event_On_Error()
    {
        var authenticationResult = new AuthenticationResult();

        _playerRegistrationService.Setup(x => x.Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns<string, string, string>(async (x, y, z) => authenticationResult = new AuthenticationResult { Code = AuthenticationResultCode.Error });

        var raised = false;

        _processor.PlayerAuthenticationFailed += (sender, args) =>
        {
            raised = true;
        };

        _processor.RegisterPlayer(_displayName, _email, _password);

        Assert.IsTrue(raised);
    }

    [TestCase(null, null, "password")]
    [TestCase(null, "email@email.com", null)]
    [TestCase("diplayName", null, null)]
    [TestCase(null, null, null)]
    public void Should_Not_Raise_Event_When_DisplayName_Or_Email_Or_Password_Is_Not_Assigned(string displayName, string email, string password)
    {
        var raised = false;

        _processor.PlayerAuthenticated += (sender, args) =>
        {
            raised = true;
        };

        _processor.PlayerAuthenticationFailed += (sender, args) =>
        {
            raised = true;
        };

        Assert.Throws<ArgumentNullException>(() => _processor.RegisterPlayer(_displayName, email, password));
        Assert.IsFalse(raised);
    }

    [Test]
    public void Should_Return_Player_On_Success()
    {
        var authenticationResult = new AuthenticationResult();

        _playerRegistrationService.Setup(x => x.Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns<string, string, string>(async (x, y, z) => authenticationResult = new AuthenticationResult { Code = AuthenticationResultCode.Success, Player = _player });

        _processor.RegisterPlayer(_displayName, _email, _password);

        Assert.AreSame(authenticationResult.Player, _player);
        Assert.IsNotNull(authenticationResult.Player);
    }

    [Test]
    public void Should_Not_Return_Player_On_Error()
    {
        var authenticationResult = new AuthenticationResult();

        _playerRegistrationService.Setup(x => x.Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns<string, string, string>(async (x, y, z) => authenticationResult = new AuthenticationResult { Code = AuthenticationResultCode.Error });

        _processor.RegisterPlayer(_displayName, _email, _password);

        Assert.AreNotSame(authenticationResult.Player, _player);
        Assert.IsNull(authenticationResult.Player);
    }
}
