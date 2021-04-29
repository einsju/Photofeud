using Firebase;
using Firebase.Auth;
using Photofeud.Abstractions;
using Photofeud.Authentication;
using System.Threading.Tasks;
using UnityEngine;

namespace Photofeud.Firebase
{
    public class PlayerRegistrationService : MonoBehaviour, IPlayerRegistrationService
    {
        ITranslator _translator;

        void Awake()
        {
            _translator = GetComponent<ITranslator>();
        }

        public async Task<AuthenticationResult> Register(string email, string password)
        {
            var result = new AuthenticationResult { Code = AuthenticationResultCode.Success };

            await FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    var firebaseException = task.Exception.GetBaseException() as FirebaseException;

                    result.Code = AuthenticationResultCode.Error;
                    result.ErrorMessage = _translator.Translate($"{firebaseException.ErrorCode}");
                    return;
                }

                var user = task.Result;
                result.Player = new Player(user.UserId, user.DisplayName, user.Email);
            });

            return result;
        }
    }
}
