using Moq;
using NUnit.Framework;
using Photofeud.Abstractions;
using Photofeud.Authentication;
using System;

namespace Photofeud
{
    public class ProfileUpdateProcessorTests
    {
        readonly string _email = "email@email.com";
        readonly string _password = "password";
        readonly ProfileUpdateProcessor _processor;
        readonly Mock<IProfileUpdateService> _profileUpdateService;

        public ProfileUpdateProcessorTests()
        {
            _profileUpdateService = new Mock<IProfileUpdateService>();
            _processor = new ProfileUpdateProcessor(_profileUpdateService.Object);
        }

        [Test]
        public void ResetPassword_Should_Throw_Exception_When_Email_Is_Not_Assigned()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.ResetPassword(null));

            Assert.AreEqual("email", exception.ParamName);
        }

        [Test]
        public void UpdatePassword_Should_Throw_Exception_When_Password_Is_Not_Assigned()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.UpdatePassword(null));

            Assert.AreEqual("password", exception.ParamName);
        }

        [Test]
        public void ResetPassword_Should_Raise_Expected_Event_On_Success()
        {
            var authenticationResult = new AuthenticationResult();

            _profileUpdateService.Setup(x => x.ResetPassword(_email))
                .Returns(async () => authenticationResult = new AuthenticationResult { Code = AuthenticationResultCode.Success });

            var raised = false;

            _processor.ProfileUpdated += (sender, args) =>
            {
                raised = true;
            };

            _processor.ResetPassword(_email);

            Assert.IsTrue(raised);
        }

        [Test]
        public void UpdatePassword_Should_Raise_Expected_Event_On_Success()
        {
            var authenticationResult = new AuthenticationResult();

            _profileUpdateService.Setup(x => x.UpdatePassword(_password))
                .Returns(async () => authenticationResult = new AuthenticationResult { Code = AuthenticationResultCode.Success });

            var raised = false;

            _processor.ProfileUpdated += (sender, args) =>
            {
                raised = true;
            };

            _processor.UpdatePassword(_password);

            Assert.IsTrue(raised);
        }

        [Test]
        public void ResetPassword_Should_Not_Raise_Event_When_Email_Is_Not_Assigned()
        {
            var raised = false;

            _processor.ProfileUpdated += (sender, args) =>
            {
                raised = true;
            };

            _processor.ProfileUpdateFailed += (sender, args) =>
            {
                raised = true;
            };

            Assert.Throws<ArgumentNullException>(() => _processor.ResetPassword(null));
            Assert.IsFalse(raised);
        }

        [Test]
        public void UpdatePassword_Should_Not_Raise_Event_When_Email_Is_Not_Assigned()
        {
            var raised = false;

            _processor.ProfileUpdated += (sender, args) =>
            {
                raised = true;
            };

            _processor.ProfileUpdateFailed += (sender, args) =>
            {
                raised = true;
            };

            Assert.Throws<ArgumentNullException>(() => _processor.UpdatePassword(null));
            Assert.IsFalse(raised);
        }
    }
}
