using FSL.Framework.Core.Models;
using System.Threading.Tasks;

namespace FSL.Framework.Core.Service
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> AuthenticateAsync(
            IUser user);

        string GetAuthenticationSchema();

        Task LogoutAsync();
    }
}
