using Photofeud.Abstractions;
using System;
using System.Threading.Tasks;

namespace Photofeud.Authentication
{
    public class LoginProcessor : AuthenticationProcessor
    {
        const string InvalidEmail = "Invalid Email";
        const string InvalidPassword = "Invalid Password";

        IAuthenticationService _authenticationService;

        bool IsFieldAssigned(string field) => !string.IsNullOrEmpty(field);

        public LoginProcessor(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public void LoginPlayer(string email, string password)
        {
            ValidateSignInInput(email, password);
            _ = Login(email, password);
        }

        void ValidateSignInInput(string email, string password)
        {
            ThrowArgumentNullExceptionOnInvalidData(email, nameof(email), InvalidEmail);
            ThrowArgumentNullExceptionOnInvalidData(password, nameof(password), InvalidPassword);
        }

        void ThrowArgumentNullExceptionOnInvalidData(string data, string paramName, string message)
        {
            if (!IsFieldAssigned(data))
                throw new ArgumentNullException(paramName, message);
        }

        async Task Login(string email, string password)
        {
            var result = await _authenticationService.Login(email, password);

            if (result.Code != AuthenticationResultCode.Success)
            {
                OnPlayerAuthenticationFailed(result.ErrorMessage);
                return;
            }

            OnPlayerAuthenticated();
        }
    }
}
