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
    public class PlayerLoginService : MonoBehaviour, IPlayerLoginService
    {
        ITranslator _translator;

        void Awake()
        {
            _translator = GetComponent<ITranslator>();
        }

        public async Task<AuthenticationResult> Login(string email, string password)
        {
            var result = new AuthenticationResult { Code = AuthenticationResultCode.Success };

            await FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
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
