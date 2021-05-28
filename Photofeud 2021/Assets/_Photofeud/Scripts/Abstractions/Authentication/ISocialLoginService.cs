using Photofeud.Authentication;
using System.Threading.Tasks;

namespace Photofeud.Abstractions.Authentication
{
    public interface ISocialLoginService
    {
        Task<AuthenticationResult> Login(SocialLoginProvider provider = SocialLoginProvider.Google);
    }
}
