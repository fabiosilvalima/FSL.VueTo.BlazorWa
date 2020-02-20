using FSL.Framework.Core.Models;
using System.Threading.Tasks;

namespace FSL.Framework.Core.Authorization.Service
{
    public interface IAuthorizationService
    {
        Task<BaseResult<IUser>> AuthorizeAsync(
            LoginUser loginUser);
    }
}
