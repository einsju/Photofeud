using Photofeud.Authentication;
using System.Threading.Tasks;

namespace Photofeud.Abstractions
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> Register(string displayName, string email, string password);
        Task<AuthenticationResult> Login(string email, string password);
        Task<AuthenticationResult> LoginAsGuest();
        Task<AuthenticationResult> LoginWithSocialProvider(SocialLoginProvider provider = SocialLoginProvider.Google);
        AuthenticationResult Logout();
    }
}
