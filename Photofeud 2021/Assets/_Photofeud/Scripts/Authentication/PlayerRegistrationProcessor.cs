using System;
using System.Threading.Tasks;

namespace Photofeud.Authentication
{
    public class PlayerRegistrationProcessor : PlayerAuthenticationProcessor
    {
        const string InvalidDisplayName = "Invalid Email";
        const string InvalidEmail = "Invalid Email";
        const string InvalidPassword = "Invalid Password";

        IPlayerRegistrationService _playerRegistrationService;

        bool IsFieldAssigned(string field) => !string.IsNullOrEmpty(field);

        public PlayerRegistrationProcessor(IPlayerRegistrationService playerRegistrationService)
        {
            _playerRegistrationService = playerRegistrationService;
        }

        public void RegisterPlayer(string displayName, string email, string password)
        {
            ValidateSignUpInput(displayName, email, password);
            _ = Register(displayName, email, password);
        }

        void ValidateSignUpInput(string displayName, string email, string password)
        {
            ThrowArgumentNullExceptionOnInvalidData(displayName, nameof(displayName), InvalidDisplayName);
            ThrowArgumentNullExceptionOnInvalidData(email, nameof(email), InvalidEmail);
            ThrowArgumentNullExceptionOnInvalidData(password, nameof(password), InvalidPassword);
        }

        void ThrowArgumentNullExceptionOnInvalidData(string data, string paramName, string message)
        {
            if (!IsFieldAssigned(data))
                throw new ArgumentNullException(paramName, message);
        }

        async Task Register(string displayName, string email, string password)
        {
            var result = await _playerRegistrationService.Register(displayName, email, password);

            if (result.Code != AuthenticationResultCode.Success)
            {
                OnPlayerAuthenticationFailed(result.ErrorMessage);
                return;
            }

            OnPlayerAuthenticated();
        }
    }
}