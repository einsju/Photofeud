using Photofeud.Abstractions;
using System;
using System.Threading.Tasks;

namespace Photofeud.Authentication
{
    public class ProfileUpdateProcessor
    {
        public event EventHandler ProfileUpdated;
        public event EventHandler<string> ProfileUpdateFailed;

        const string InvalidEmail = "Invalid Email";
        const string InvalidPassword = "Invalid Password";

        IProfileUpdateService _profileUpdateService;

        bool IsFieldAssigned(string field) => !string.IsNullOrEmpty(field);

        public ProfileUpdateProcessor(IProfileUpdateService profileUpdateService)
        {
            _profileUpdateService = profileUpdateService;
        }

        public void ResetPassword(string email)
        {
            ValidateEmailInput(email);
            _ = Reset(email);
        }

        void ValidateEmailInput(string email)
        {
            ThrowArgumentNullExceptionOnInvalidData(email, nameof(email), InvalidEmail);
        }

        async Task Reset(string email)
        {
            HandleEvents(await _profileUpdateService.ResetPassword(email));
        }

        public void UpdatePassword(string password)
        {
            ValidatePasswordInput(password);
            _ = Update(password);
        }

        void ValidatePasswordInput(string password)
        {
            ThrowArgumentNullExceptionOnInvalidData(password, nameof(password), InvalidPassword);
        }

        async Task Update(string password)
        {
            HandleEvents(await _profileUpdateService.UpdatePassword(password));
        }

        void ThrowArgumentNullExceptionOnInvalidData(string data, string paramName, string message)
        {
            if (!IsFieldAssigned(data))
                throw new ArgumentNullException(paramName, message);
        }        

        void HandleEvents(AuthenticationResult result)
        {
            if (result.Code != AuthenticationResultCode.Success)
            {
                ProfileUpdateFailed?.Invoke(this, result.ErrorMessage);
                return;
            }

            ProfileUpdated?.Invoke(this, null);
        }
    }
}
