using FSL.Framework.Core.ApiClient.Models;
using System.Threading.Tasks;

namespace FSL.Framework.Core.ApiClient.Provider
{
    public interface IApiClientProvider
    {
        IApiClientProvider UseAuthenticationBearer(
            string bearerToken = null);

        IApiClientProvider UseJsonContentType();

        IApiClientProvider UseBaseUrl(
            string baseUrl);

        Task<ApiClientResult<T>> GetAsync<T>(
            string apiRoute)
            where T : new();

        Task<ApiClientResult<T>> PostAsync<T>(
            string apiRoute,
            object body)
            where T : new();
    }
}
