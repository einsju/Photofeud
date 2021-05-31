using Photofeud.Authentication;
using System.Threading.Tasks;

namespace Photofeud.Abstractions
{
    public interface IProfileUpdateService
    {
        Task<AuthenticationResult> ResetPassword(string email);
        Task<AuthenticationResult> UpdatePassword(string password);
    }
}
