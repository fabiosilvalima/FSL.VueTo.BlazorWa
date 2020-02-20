using FSL.Framework.Core.Models;
using System.Threading.Tasks;

namespace FSL.Framework.Core.Authentication.Service
{
    public interface ILoggedUserService
    {
        Task<BaseResult<T>> GetLoggedUserAsync<T>()
            where T : class, IUser;
    }
}
