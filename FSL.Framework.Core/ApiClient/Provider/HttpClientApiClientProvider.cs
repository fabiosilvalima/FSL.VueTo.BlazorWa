using FSL.Framework.Core.ApiClient.Models;
using FSL.Framework.Core.Extensions;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FSL.Framework.Core.ApiClient.Provider
{
    public sealed class HttpClientApiClientProvider : BaseApiClientProvider
    {
        private readonly HttpClient _httpClient;

        public HttpClientApiClientProvider()
        {
            _httpClient = new HttpClient();
        }

        public override async Task<ApiClientResult<T>> GetAsync<T>(
            string apiRoute)
        {
            _apiRoute = apiRoute;
            
            try
            {
                AddReponseHeaders();

                var response = await _httpClient.GetAsync($"{_apiUrlBase}{apiRoute}");
                
                return await ConvertResponseToApiClientResultAsync<T>(response: response);
            }
            catch (Exception ex)
            {
                return await ConvertResponseToApiClientResultAsync<T>(ex: ex);
            }
        }

        public override async Task<ApiClientResult<T>> PostAsync<T>(
            string apiRoute,
            object body)
        {
            _apiRoute = apiRoute;

            try
            {
                AddReponseHeaders();
                UseJsonContentType();

                var response = await _httpClient.PostAsync(
                    $"{_apiUrlBase}{apiRoute}", 
                    new StringContent(
                        body.ToJson(), 
                        Encoding.UTF8,
                        _contentType));

                return await ConvertResponseToApiClientResultAsync<T>(response: response);
            }
            catch (Exception ex)
            {
                return await ConvertResponseToApiClientResultAsync<T>(ex: ex);
            }
        }

        private void AddReponseHeaders()
        {
            AddBearerTokenHeader();

            foreach (var header in _headers)
            {
                _httpClient.DefaultRequestHeaders.Add(
                    header.Key,
                    header.Value);
            }
        }
    }
}
