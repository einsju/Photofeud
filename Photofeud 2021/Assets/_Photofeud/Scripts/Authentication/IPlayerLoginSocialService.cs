using System.Threading.Tasks;

namespace Photofeud.Authentication
{
    public interface IPlayerLoginSocialService
    {
        Task<AuthenticationResult> Login(SocialLoginProvider provider = SocialLoginProvider.Google);
    }
}
