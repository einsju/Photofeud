using Photofeud.Authentication;
using System.Threading.Tasks;

namespace Photofeud.Abstractions.Authentication
{
    public interface IPlayerLoginGuestService
    {
        Task<AuthenticationResult> Login();
    }
}
