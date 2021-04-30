using System.Threading.Tasks;

namespace Photofeud.Authentication
{
    public interface IPlayerLoginService
    {
        Task<AuthenticationResult> Login(string email, string password);
    }
}
