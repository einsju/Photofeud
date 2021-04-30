using System;
using System.Threading.Tasks;

namespace Photofeud.Authentication
{
    public class PlayerLoginProcessor : PlayerAuthenticationProcessor
    {
        const string InvalidEmail = "Invalid Email";
        const string InvalidPassword = "Invalid Password";

        IPlayerLoginService _playerLoginService;

        bool IsFieldAssigned(string field) => !string.IsNullOrEmpty(field);

        public PlayerLoginProcessor(IPlayerLoginService playerLoginService)
        {
            _playerLoginService = playerLoginService;
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
            var result = await _playerLoginService.Login(email, password);

            if (result.Code != AuthenticationResultCode.Success)
            {
                OnPlayerAuthenticationFailed(result.ErrorMessage);
                return;
            }

            OnPlayerAuthenticated();
        }
    }
}
