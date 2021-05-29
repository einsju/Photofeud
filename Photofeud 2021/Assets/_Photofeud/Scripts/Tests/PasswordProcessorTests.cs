using Moq;
using NUnit.Framework;
using Photofeud.Abstractions;
using Photofeud.Authentication;

namespace Photofeud
{
    public class PasswordProcessorTests
    {
        readonly string email = "email@email.com";
        readonly PasswordProcessor _processor;
        readonly Mock<IAuthenticationService> _authenticationService;

        public PasswordProcessorTests()
        {
            _authenticationService = new Mock<IAuthenticationService>();
            _processor = new PasswordProcessor(_authenticationService.Object);
        }

        //[Test]
        //public void Should_Raise_Expected_Event_On_Success()
        //{
        //    var authenticationResult = new AuthenticationResult();

        //    _authenticationService.Setup(x => x.LoginWithSocialProvider(_provider))
        //        .Returns(async () => authenticationResult = new AuthenticationResult { Code = AuthenticationResultCode.Success });

        //    var raised = false;

        //    _processor.PlayerAuthenticated += (sender, args) =>
        //    {
        //        raised = true;
        //    };

        //    _processor.LoginPlayer(_provider);

        //    Assert.IsTrue(raised);
        //}

        //[Test]
        //public void Should_Raise_Expected_Event_On_Error()
        //{
        //    var authenticationResult = new AuthenticationResult();

        //    _authenticationService.Setup(x => x.LoginWithSocialProvider(_provider))
        //        .Returns(async () => authenticationResult = new AuthenticationResult { Code = AuthenticationResultCode.Error });

        //    var raised = false;

        //    _processor.PlayerAuthenticationFailed += (sender, args) =>
        //    {
        //        raised = true;
        //    };

        //    _processor.LoginPlayer(_provider);

        //    Assert.IsTrue(raised);
        //}

        //[Test]
        //public void Should_Return_Player_On_Success()
        //{
        //    var authenticationResult = new AuthenticationResult();

        //    _authenticationService.Setup(x => x.LoginWithSocialProvider(_provider))
        //        .Returns(async () => authenticationResult = new AuthenticationResult { Code = AuthenticationResultCode.Success, Player = _player });

        //    _processor.LoginPlayer(_provider);

        //    Assert.AreSame(authenticationResult.Player, _player);
        //    Assert.IsNotNull(authenticationResult.Player);
        //}

        //[Test]
        //public void Should_Not_Return_Player_On_Error()
        //{
        //    var authenticationResult = new AuthenticationResult();

        //    _authenticationService.Setup(x => x.LoginWithSocialProvider(_provider))
        //        .Returns(async () => authenticationResult = new AuthenticationResult { Code = AuthenticationResultCode.Error });

        //    _processor.LoginPlayer(_provider);

        //    Assert.AreNotSame(authenticationResult.Player, _player);
        //    Assert.IsNull(authenticationResult.Player);
        //}
    }
}
