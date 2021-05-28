using Photofeud.Abstractions.Authentication;
using System.Threading.Tasks;

namespace Photofeud.Authentication
{
    public class SocialLoginProcessor : AuthenticationProcessor
    {
        ISocialLoginService _loginService;

        public SocialLoginProcessor(ISocialLoginService loginService)
        {
            _loginService = loginService;
        }

        public void LoginPlayer(SocialLoginProvider provider)
        {
            _ = Login(provider);
        }

        async Task Login(SocialLoginProvider provider)
        {
            var result = await _loginService.Login(provider);

            if (result.Code != AuthenticationResultCode.Success)
            {
                OnPlayerAuthenticationFailed(result.ErrorMessage);
                return;
            }

            OnPlayerAuthenticated();
        }
    }
}
