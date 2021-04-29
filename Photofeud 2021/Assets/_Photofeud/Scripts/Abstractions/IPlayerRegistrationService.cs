using Photofeud.Authentication;
using System.Threading.Tasks;

namespace Photofeud.Abstractions
{
    public interface IPlayerRegistrationService
    {
        Task<AuthenticationResult> Register(string email, string password);
    }
}