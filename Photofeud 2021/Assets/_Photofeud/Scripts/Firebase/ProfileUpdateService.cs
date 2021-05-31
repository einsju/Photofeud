using Firebase.Auth;
using Photofeud.Abstractions;
using Photofeud.Authentication;
using Photofeud.Firebase;
using System.Threading.Tasks;
using UnityEngine;

namespace Photofeud
{
    public class ProfileUpdateService : FirebaseService, IProfileUpdateService
    {
        public async Task<AuthenticationResult> ResetPassword(string email)
        {
            var result = new AuthenticationResult { Code = AuthenticationResultCode.Success };

            await FirebaseAuth.DefaultInstance.SendPasswordResetEmailAsync(email).ContinueWith(task =>
            {
                if (!TaskIsOk(task))
                {
                    result = FirebaseError.AuthenticationError(task.Exception, _translator);
                    return;
                }
            });

            return result;
        }

        public async Task<AuthenticationResult> UpdatePassword(string password)
        {
            var result = new AuthenticationResult { Code = AuthenticationResultCode.Success };

            await FirebaseAuth.DefaultInstance.CurrentUser.UpdatePasswordAsync(password).ContinueWith(task =>
            {
                if (!TaskIsOk(task))
                {
                    result = FirebaseError.AuthenticationError(task.Exception, _translator);
                    return;
                }
            });

            return result;
        }
    }
}
