using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FSL.Framework.Core.ApiClient.Models;
using FSL.Framework.Core.Extensions;
using FSL.Framework.Core.Models;

namespace FSL.Framework.Core.ApiClient.Provider
{
    public abstract class BaseApiClientProvider : IApiClientProvider
    {
        public BaseApiClientProvider()
        {
            _headers = new Dictionary<string, string>();
            _params = new Dictionary<string, string>();
        }

        protected string _apiUrlBase;
        protected string _apiRoute;
        protected string _bearerToken;
        protected string _contentType;
        protected readonly Dictionary<string, string> _headers;
        protected readonly Dictionary<string, string> _params;
        protected string _accessToken;
        protected string _accessKey;

        public abstract Task<ApiClientResult<T>> GetAsync<T>(
            string apiRoute) where T : new();

        public virtual IApiClientProvider UseAuthenticationBearer(
            string bearerToken = null)
        {
            _bearerToken = bearerToken;

            if (string.IsNullOrEmpty(_bearerToken))
            {
                _bearerToken = GetAuthorization();
            }

            return this;
        }

        public virtual IApiClientProvider UseBaseUrl(
            string baseUrl)
        {
            _apiUrlBase = baseUrl;

            return this;
        }

        public virtual IApiClientProvider UseJsonContentType()
        {
            _contentType = "application/json";

            return this;
        }

        protected void AddBearerTokenHeader()
        {
            AddAuthenticationHeader(
                "Bearer",
                _bearerToken);
        }

        protected void AddAuthenticationHeader(
            string authenticationType,
            string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                UseHeader(
                    "Authorization",
                    $"{authenticationType} " + token);
            }
        }

        protected virtual IApiClientProvider UseHeader(
            string key,
            string value)
        {
            _headers.Add(
                key,
                value);

            return this;
        }

        protected virtual string GetAuthorization()
        {
            return "";
        }


        protected async Task<ApiClientResult<T>> ConvertResponseToApiClientResultAsync<T>(
            HttpResponseMessage response = null,
            Exception ex = null)
        {
            string content = null;

            if (response != null)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            var result = new ApiClientResult<T>()
            {
                ApiUrlBase = _apiUrlBase,
                ApiRoute = _apiRoute,
                Content = content,
                ErrorMessage = ex?.ToString(),
                StatusCode = response?.StatusCode ?? System.Net.HttpStatusCode.InternalServerError,
                Url = $"{_apiUrlBase}{_apiRoute}",
                Headers = _headers
            };

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseResult = result.Content.FromJson<BaseResult<T>>();

                if (responseResult?.Data.IsNotNull() ?? false)
                {
                    result.Success = responseResult.Success;
                    result.Message = responseResult.Message;
                    result.Data = responseResult.Data;
                }
                else
                {
                    result.Data = result.Content.FromJson<T>();
                    result.Success = result.Data.IsNotNull();
                }
            }
            else
            {
                result.Success = false;
            }

            return result;
        }

        public abstract Task<ApiClientResult<T>> PostAsync<T>(
            string apiRoute, 
            object body) where T : new();
    }
}
