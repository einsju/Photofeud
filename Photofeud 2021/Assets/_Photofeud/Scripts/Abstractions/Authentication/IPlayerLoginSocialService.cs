using Photofeud.Authentication;
using System.Threading.Tasks;

namespace Photofeud.Abstractions.Authentication
{
    public interface IPlayerLoginSocialService
    {
        Task<AuthenticationResult> Login(SocialLoginProvider provider = SocialLoginProvider.Google);
    }
}
