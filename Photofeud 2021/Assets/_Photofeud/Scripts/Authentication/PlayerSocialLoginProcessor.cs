using System.Threading.Tasks;

namespace Photofeud.Authentication
{
    public class PlayerSocialLoginProcessor : PlayerAuthenticationProcessor
    {
        IPlayerSocialLoginService _playerLoginService;

        public PlayerSocialLoginProcessor(IPlayerSocialLoginService playerLoginService)
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
