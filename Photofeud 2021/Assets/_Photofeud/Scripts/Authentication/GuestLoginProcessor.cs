using Photofeud.Abstractions;
using System.Threading.Tasks;

namespace Photofeud.Authentication
{
    public class GuestLoginProcessor : AuthenticationProcessor
    {
        IAuthenticationService _authenticationService;

        public GuestLoginProcessor(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public void LoginPlayerAsGuest()
        {
            _ = LoginAsGuest();
        }

        async Task LoginAsGuest()
        {
            var result = await _authenticationService.LoginAsGuest();

            if (result.Code != AuthenticationResultCode.Success)
            {
                OnPlayerAuthenticationFailed(result.ErrorMessage);
                return;
            }

            OnPlayerAuthenticated();
        }
    }
}
