using Firebase.Auth;
using Photofeud.Abstractions;
using Photofeud.Authentication;
using Photofeud.Profile;
using System.Threading.Tasks;

namespace Photofeud.Firebase
{
    public class AuthenticationService : FirebaseService, IAuthenticationService
    {
        public async Task<AuthenticationResult> Login(string email, string password)
        {
            var result = new AuthenticationResult { Code = AuthenticationResultCode.Success };

            await FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (!TaskIsOk(task))
                {
                    result = FirebaseError.AuthenticationError(task.Exception, _translator);
                    return;
                }

                var user = task.Result;
                result.Player = new Player(user.UserId, user.DisplayName, user.Email);
            });

            return result;
        }

        public async Task<AuthenticationResult> LoginAsGuest()
        {
            var result = new AuthenticationResult { Code = AuthenticationResultCode.Success };

            await FirebaseAuth.DefaultInstance.SignInAnonymouslyAsync().ContinueWith(task =>
            {
                if (!TaskIsOk(task))
                {
                    result = FirebaseError.AuthenticationError(task.Exception, _translator);
                    return;
                }

                var user = task.Result;
                result.Player = new Player(user.UserId, user.DisplayName, user.Email);
            });

            return result;
        }

        public async Task<AuthenticationResult> LoginWithSocialProvider(SocialLoginProvider provider = SocialLoginProvider.Google)
        {
            return provider switch
            {
                SocialLoginProvider.Google => await LoginWithGoogle(),
                SocialLoginProvider.Facebook => await LoginWithFacebook(),
                _ => await LoginWithGoogle(),
            };
        }

        async Task<AuthenticationResult> LoginWithGoogle()
        {
            var result = new AuthenticationResult { Code = AuthenticationResultCode.Success };

            //await FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            //{
            //    if (!TaskIsOk(task))
            //    {
            //        result = FirebaseError.AuthenticationError(task.Exception, _translator);
            //        return;
            //    }

            //    var user = task.Result;
            //    result.Player = new Player(user.UserId, user.DisplayName, user.Email);
            //});

            return result;
        }

        async Task<AuthenticationResult> LoginWithFacebook()
        {
            var result = new AuthenticationResult { Code = AuthenticationResultCode.Success };

            //await FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            //{
            //    if (!TaskIsOk(task))
            //    {
            //        result = FirebaseError.AuthenticationError(task.Exception, _translator);
            //        return;
            //    }

            //    var user = task.Result;
            //    result.Player = new Player(user.UserId, user.DisplayName, user.Email);
            //});

            return result;
        }

        public AuthenticationResult Logout()
        {
            FirebaseAuth.DefaultInstance.SignOut();

            return new AuthenticationResult { Code = AuthenticationResultCode.Success, Player = null };
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
                if (!TaskIsOk(task))
                {
                    result = FirebaseError.AuthenticationError(task.Exception, _translator);
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
                if (!TaskIsOk(task))
                {
                    result = FirebaseError.AuthenticationError(task.Exception, _translator);
                    return;
                }

                var user = FirebaseAuth.DefaultInstance.CurrentUser;
                result.Player = new Player(user.UserId, user.DisplayName, user.Email);
            });

            return result;
        }
    }
}
