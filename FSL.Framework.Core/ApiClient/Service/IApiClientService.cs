using FSL.Framework.Core.ApiClient.Provider;
using System.Threading.Tasks;

namespace FSL.Framework.Core.ApiClient.Service
{
    public interface IApiClientService
    {
        Task<IApiClientProvider> CreateInstanceAsync();
    }
}
