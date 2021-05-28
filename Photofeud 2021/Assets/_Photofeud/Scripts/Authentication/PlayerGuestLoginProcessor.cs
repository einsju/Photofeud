using Photofeud.Abstractions.Authentication;
using System.Threading.Tasks;

namespace Photofeud.Authentication
{
    public class PlayerLoginGuestProcessor : PlayerAuthenticationProcessor
    {
        IPlayerLoginGuestService _playerLoginGuestService;

        public PlayerLoginGuestProcessor(IPlayerLoginGuestService playerLoginGuestService)
        {
            _playerLoginGuestService = playerLoginGuestService;
        }

        public void LoginPlayerAsGuest()
        {
            _ = LoginAsGuest();
        }

        async Task LoginAsGuest()
        {
            var result = await _playerLoginGuestService.Login();

            if (result.Code != AuthenticationResultCode.Success)
            {
                OnPlayerAuthenticationFailed(result.ErrorMessage);
                return;
            }

            OnPlayerAuthenticated();
        }
    }
}
