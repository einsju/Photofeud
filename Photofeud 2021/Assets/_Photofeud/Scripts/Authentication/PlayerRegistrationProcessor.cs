using Photofeud.Abstractions;
using System;
using System.Threading.Tasks;

namespace Photofeud.Authentication
{
    public class PlayerRegistrationProcessor
    {
        const string InvalidEmail = "Invalid Email";
        const string InvalidPassword = "Invalid Password";

        public event EventHandler PlayerRegistrationSucceeded;
        public event EventHandler<string> PlayerRegistrationFailed;
        
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
                PlayerRegistrationFailed?.Invoke(this, result.ErrorMessage);
                return;
            }

            PlayerRegistrationSucceeded?.Invoke(this, null);
        }
    }
}