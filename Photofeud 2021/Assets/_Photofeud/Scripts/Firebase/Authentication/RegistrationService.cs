using Firebase;
using Firebase.Auth;
using Photofeud.Abstractions.Authentication;
using Photofeud.Abstractions.Translation;
using Photofeud.Authentication;
using Photofeud.Profile;
using System.Threading.Tasks;
using UnityEngine;

namespace Photofeud.Firebase.Authentication
{
    public class RegistrationService : MonoBehaviour, IRegistrationService
    {
        ITranslator _translator;

        void Awake()
        {
            _translator = GetComponent<ITranslator>();
        }

        public async Task<AuthenticationResult> Register(string displayName, string email, string password)
        {
            var result = await CreateUserWithEmailAndPassword(email, password);

            if (result.Code == AuthenticationResultCode.Error || FirebaseAuth.DefaultInstance.CurrentUser is null)
                return result;

            result = await UpdateUserProfile(displayName);

            return result;
        }

        async Task<AuthenticationResult> CreateUserWithEmailAndPassword(string email, string password)
        {
            var result = new AuthenticationResult { Code = AuthenticationResultCode.Success };

            await FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    result = FirebaseErrorResult(task.Exception.GetBaseException() as FirebaseException);
                    return;
                }
            });

            return result;
        }

        async Task<AuthenticationResult> UpdateUserProfile(string displayName)
        {
            var result = new AuthenticationResult { Code = AuthenticationResultCode.Success };
            var profile = new UserProfile { DisplayName = displayName };

            await FirebaseAuth.DefaultInstance.CurrentUser.UpdateUserProfileAsync(profile).ContinueWith(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    result = FirebaseErrorResult(task.Exception.GetBaseException() as FirebaseException);
                    return;
                }

                var user = FirebaseAuth.DefaultInstance.CurrentUser;
                result.Player = new Player(user.UserId, user.DisplayName, user.Email);
            });

            return result;
        }

        AuthenticationResult FirebaseErrorResult(FirebaseException exception)
        {
            var firebaseException = exception.GetBaseException() as FirebaseException;
            return new AuthenticationResult { Code = AuthenticationResultCode.Error, ErrorMessage = _translator.Translate($"{firebaseException.ErrorCode}") };
        }
    }
}
