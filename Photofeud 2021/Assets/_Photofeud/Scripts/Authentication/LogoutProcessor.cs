using Photofeud.Abstractions;

namespace Photofeud.Authentication
{
    public class LogoutProcessor
    {
        IAuthenticationService _authenticationService;

        public LogoutProcessor(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public void LogoutPlayer()
        {
            _authenticationService.Logout();
        }
    }
}