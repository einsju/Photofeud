using System.Threading.Tasks;

namespace Photofeud.Authentication
{
    public interface IPlayerSocialLoginService
    {
        Task<AuthenticationResult> Login(SocialLoginProvider provider = SocialLoginProvider.Google);
    }
}
