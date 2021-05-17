using Firebase;
using Firebase.Auth;
using Photofeud.Authentication;
using Photofeud.Translation;
using System.Threading.Tasks;
using UnityEngine;

namespace Photofeud.Firebase.Authentication
{
    public class PlayerLoginSocialService : MonoBehaviour, IPlayerLoginSocialService
    {
        ITranslator _translator;

        void Awake()
        {
            _translator = GetComponent<ITranslator>();
        }

        public async Task<AuthenticationResult> Login(SocialLoginProvider provider = SocialLoginProvider.Google)
        {
            return provider switch
            {
                SocialLoginProvider.Google => await LoginGoogle(),
                SocialLoginProvider.Facebook => await LoginFacebook(),
                _ => await LoginGoogle(),
            };
        }

        async Task<AuthenticationResult> LoginGoogle()
        {
            var result = new AuthenticationResult { Code = AuthenticationResultCode.Success };

            //await FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            //{
            //    if (task.IsCanceled || task.IsFaulted)
            //    {
            //        var firebaseException = task.Exception.GetBaseException() as FirebaseException;

            //        result.Code = AuthenticationResultCode.Error;
            //        result.ErrorMessage = _translator.Translate($"{firebaseException.ErrorCode}");
            //        return;
            //    }

            //    var user = task.Result;
            //    result.Player = new Player(user.UserId, user.DisplayName, user.Email);
            //});

            return result;
        }

        async Task<AuthenticationResult> LoginFacebook()
        {
            var result = new AuthenticationResult { Code = AuthenticationResultCode.Success };

            //await FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            //{
            //    if (task.IsCanceled || task.IsFaulted)
            //    {
            //        var firebaseException = task.Exception.GetBaseException() as FirebaseException;

            //        result.Code = AuthenticationResultCode.Error;
            //        result.ErrorMessage = _translator.Translate($"{firebaseException.ErrorCode}");
            //        return;
            //    }

            //    var user = task.Result;
            //    result.Player = new Player(user.UserId, user.DisplayName, user.Email);
            //});

            return result;
        }
    }
}
