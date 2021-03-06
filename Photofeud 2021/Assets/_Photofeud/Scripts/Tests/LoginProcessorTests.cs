using Moq;
using NUnit.Framework;
using Photofeud.Abstractions;
using Photofeud.Authentication;
using Photofeud.Profile;
using System;

namespace Photofeud
{
    public class LoginProcessorTests
    {
        readonly string _email;
        readonly string _password;

        readonly LoginProcessor _processor;
        readonly Mock<IAuthenticationService> _authenticationService;
        readonly Player _player;

        public LoginProcessorTests()
        {
            _email = "email@email.com";
            _password = "password";

            _authenticationService = new Mock<IAuthenticationService>();
            _processor = new LoginProcessor(_authenticationService.Object);
            _player = new Player("Wktb8xUwmyZCtqUF7qvAGXeWPCt2", "7r78", _email);
        }

        [Test]
        public void Should_Throw_Exception_When_Email_Is_Not_Assigned()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.LoginPlayer(null, _password));

            Assert.AreEqual("email", exception.ParamName);
        }

        [Test]
        public void Should_Throw_Exception_When_Password_Is_Not_Assigned()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.LoginPlayer(_email, null));

            Assert.AreEqual("password", exception.ParamName);
        }

        [Test]
        public void Should_Raise_Expected_Event_On_Success()
        {
            var authenticationResult = new AuthenticationResult();

            _authenticationService.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>(async (x, y) => authenticationResult = new AuthenticationResult { Code = AuthenticationResultCode.Success });

            var raised = false;

            _processor.PlayerAuthenticated += (sender, args) =>
            {
                raised = true;
            };

            _processor.LoginPlayer(_email, _password);

            Assert.IsTrue(raised);
        }

        [Test]
        public void Should_Raise_Expected_Event_On_Error()
        {
            var authenticationResult = new AuthenticationResult();

            _authenticationService.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>(async (x, y) => authenticationResult = new AuthenticationResult { Code = AuthenticationResultCode.Error });

            var raised = false;

            _processor.PlayerAuthenticationFailed += (sender, args) =>
            {
                raised = true;
            };

            _processor.LoginPlayer(_email, _password);

            Assert.IsTrue(raised);
        }

        [TestCase(null, "password")]
        [TestCase("email@email.com", null)]
        [TestCase(null, null)]
        public void Should_Not_Raise_Event_When_Email_Or_Password_Is_Not_Assigned(string email, string password)
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

            Assert.Throws<ArgumentNullException>(() => _processor.LoginPlayer(email, password));
            Assert.IsFalse(raised);
        }

        [Test]
        public void Should_Return_Player_On_Success()
        {
            var authenticationResult = new AuthenticationResult();

            _authenticationService.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>(async (x, y) => authenticationResult = new AuthenticationResult { Code = AuthenticationResultCode.Success, Player = _player });

            _processor.LoginPlayer(_email, _password);

            Assert.AreSame(authenticationResult.Player, _player);
            Assert.IsNotNull(authenticationResult.Player);
        }

        [Test]
        public void Should_Not_Return_Player_On_Error()
        {
            var authenticationResult = new AuthenticationResult();

            _authenticationService.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>(async (x, y) => authenticationResult = new AuthenticationResult { Code = AuthenticationResultCode.Error });

            _processor.LoginPlayer(_email, _password);

            Assert.AreNotSame(authenticationResult.Player, _player);
            Assert.IsNull(authenticationResult.Player);
        }
    }
}
