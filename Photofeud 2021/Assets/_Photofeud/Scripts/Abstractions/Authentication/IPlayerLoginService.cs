using Photofeud.Authentication;
using System.Threading.Tasks;

namespace Photofeud.Abstractions.Authentication
{
    public interface IPlayerLoginService
    {
        Task<AuthenticationResult> Login(string email, string password);
    }
}
