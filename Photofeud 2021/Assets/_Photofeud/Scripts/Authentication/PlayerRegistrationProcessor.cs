using System;
using System.Threading.Tasks;

namespace Photofeud.Authentication
{
    public class PlayerRegistrationProcessor : PlayerAuthenticationProcessor
    {
        const string InvalidEmail = "Invalid Email";
        const string InvalidPassword = "Invalid Password";

        IPlayerRegistrationService _playerRegistrationService;

        bool IsFieldAssigned(string field) => !string.IsNullOrEmpty(field);

        public PlayerRegistrationProcessor(IPlayerRegistrationService playerRegistrationService)
        {
            _playerRegistrationService = playerRegistrationService;
        }

        public void RegisterPlayer(string email, string password)
        {
            ValidateSignUpInput(email, password);
            _ = Register(email, password);
        }

        void ValidateSignUpInput(string email, string password)
        {
            ThrowArgumentNullExceptionOnInvalidData(email, nameof(email), InvalidEmail);
            ThrowArgumentNullExceptionOnInvalidData(password, nameof(password), InvalidPassword);
        }

        void ThrowArgumentNullExceptionOnInvalidData(string data, string paramName, string message)
        {
            if (!IsFieldAssigned(data))
                throw new ArgumentNullException(paramName, message);
        }

        async Task Register(string email, string password)
        {
            var result = await _playerRegistrationService.Register(email, password);

            if (result.Code != AuthenticationResultCode.Success)
            {
                OnPlayerAuthenticationFailed(result.ErrorMessage);
                return;
            }

            OnPlayerAuthenticated();
        }
    }
}