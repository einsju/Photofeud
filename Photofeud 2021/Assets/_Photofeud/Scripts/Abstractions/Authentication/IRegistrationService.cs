using Photofeud.Authentication;
using System.Threading.Tasks;

namespace Photofeud.Abstractions.Authentication
{
    public interface IRegistrationService
    {
        Task<AuthenticationResult> Register(string displayName, string email, string password);
    }
}