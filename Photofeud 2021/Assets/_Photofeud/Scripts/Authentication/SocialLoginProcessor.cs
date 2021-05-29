using Photofeud.Abstractions;
using System.Threading.Tasks;

namespace Photofeud.Authentication
{
    public class SocialLoginProcessor : AuthenticationProcessor
    {
        IAuthenticationService _authenticationService;

        public SocialLoginProcessor(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public void LoginPlayer(SocialLoginProvider provider)
        {
            _ = Login(provider);
        }

        async Task Login(SocialLoginProvider provider)
        {
            var result = await _authenticationService.LoginWithSocialProvider(provider);

            if (result.Code != AuthenticationResultCode.Success)
            {
                OnPlayerAuthenticationFailed(result.ErrorMessage);
                return;
            }

            OnPlayerAuthenticated();
        }
    }
}
