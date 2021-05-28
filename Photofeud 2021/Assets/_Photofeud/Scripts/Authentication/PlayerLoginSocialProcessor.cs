using Photofeud.Abstractions.Authentication;
using System.Threading.Tasks;

namespace Photofeud.Authentication
{
    public class PlayerLoginSocialProcessor : PlayerAuthenticationProcessor
    {
        IPlayerLoginSocialService _playerLoginService;

        public PlayerLoginSocialProcessor(IPlayerLoginSocialService playerLoginService)
        {
            _playerLoginService = playerLoginService;
        }

        public void LoginPlayer(SocialLoginProvider provider)
        {
            _ = Login(provider);
        }

        async Task Login(SocialLoginProvider provider)
        {
            var result = await _playerLoginService.Login(provider);

            if (result.Code != AuthenticationResultCode.Success)
            {
                OnPlayerAuthenticationFailed(result.ErrorMessage);
                return;
            }

            OnPlayerAuthenticated();
        }
    }
}
