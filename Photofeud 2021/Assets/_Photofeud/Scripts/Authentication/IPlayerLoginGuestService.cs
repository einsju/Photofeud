using System.Threading.Tasks;

namespace Photofeud.Authentication
{
    public interface IPlayerLoginGuestService
    {
        Task<AuthenticationResult> Login();
    }
}
