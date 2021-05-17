using System.Threading.Tasks;

namespace Photofeud.Authentication
{
    public interface IPlayerRegistrationService
    {
        Task<AuthenticationResult> Register(string displayName, string email, string password);
    }
}