using Photofeud.Authentication;
using System.Threading.Tasks;

namespace Photofeud.Abstractions.Authentication
{
    public interface IGuestLoginService
    {
        Task<AuthenticationResult> Login();
    }
}
