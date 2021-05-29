using Photofeud.Abstractions;
using System;
using System.Threading.Tasks;

namespace Photofeud.Authentication
{
    public class PasswordProcessor
    {
        public event EventHandler PasswordHandled;
        public event EventHandler<string> PasswordHandleFailed;

        const string InvalidEmail = "Invalid Email";

        IAuthenticationService _authenticationService;

        bool IsFieldAssigned(string field) => !string.IsNullOrEmpty(field);

        public PasswordProcessor(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public void ResetPassword(string email)
        {
            ValidateInput(email);
            _ = Reset(email);
        }

        void ValidateInput(string email)
        {
            ThrowArgumentNullExceptionOnInvalidData(email, nameof(email), InvalidEmail);
        }

        void ThrowArgumentNullExceptionOnInvalidData(string data, string paramName, string message)
        {
            if (!IsFieldAssigned(data))
                throw new ArgumentNullException(paramName, message);
        }

        async Task Reset(string email)
        {
            var result = await _authenticationService.ResetPassword(email);

            if (result.Code != AuthenticationResultCode.Success)
            {
                PasswordHandleFailed?.Invoke(this, result.ErrorMessage);
                return;
            }

            PasswordHandled?.Invoke(this, null);
        }
    }
}
