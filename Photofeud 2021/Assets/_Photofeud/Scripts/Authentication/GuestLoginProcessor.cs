using Photofeud.Abstractions.Authentication;
using System.Threading.Tasks;

namespace Photofeud.Authentication
{
    public class GuestLoginProcessor : AuthenticationProcessor
    {
        IGuestLoginService _loginService;

        public GuestLoginProcessor(IGuestLoginService loginService)
        {
            _loginService = loginService;
        }

        public void LoginPlayerAsGuest()
        {
            _ = LoginAsGuest();
        }

        async Task LoginAsGuest()
        {
            var result = await _loginService.Login();

            if (result.Code != AuthenticationResultCode.Success)
            {
                OnPlayerAuthenticationFailed(result.ErrorMessage);
                return;
            }

            OnPlayerAuthenticated();
        }
    }
}
