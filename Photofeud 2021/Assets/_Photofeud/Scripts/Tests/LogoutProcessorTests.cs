using Moq;
using NUnit.Framework;
using Photofeud.Abstractions;
using Photofeud.Authentication;
using Photofeud.Profile;

namespace Photofeud
{
    public class LogoutProcessorTests
    {
        readonly LogoutProcessor _processor;
        readonly Mock<IAuthenticationService> _authenticationService;
        readonly Player _player;

        public LogoutProcessorTests()
        {
            _authenticationService = new Mock<IAuthenticationService>();
            _processor = new LogoutProcessor(_authenticationService.Object);
            _player = new Player("Wktb8xUwmyZCtqUF7qvAGXeWPCt2", "7r78", "email@email.com");
        }

        [Test]
        public void Should_Reset_Player_On_Success()
        {
            var authenticationResult = new AuthenticationResult();

            _authenticationService.Setup(x => x.Logout())
                .Returns(() => authenticationResult = new AuthenticationResult { Code = AuthenticationResultCode.Success, Player = null });

            _processor.LogoutPlayer();

            Assert.IsNull(authenticationResult.Player);
        }

        [Test]
        public void Should_Not_Reset_Player_On_Error()
        {
            var authenticationResult = new AuthenticationResult();

            _authenticationService.Setup(x => x.Logout())
                .Returns(() => authenticationResult = new AuthenticationResult { Code = AuthenticationResultCode.Error, Player = _player });

            _processor.LogoutPlayer();

            Assert.IsNotNull(authenticationResult.Player);
            Assert.AreSame(authenticationResult.Player, _player);
        }
    }
}
