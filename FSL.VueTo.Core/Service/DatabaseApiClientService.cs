using FSL.VueTo.Core.Models;
using FSL.Framework.Core.ApiClient.Provider;
using FSL.Framework.Core.ApiClient.Service;
using FSL.Framework.Core.Factory.Service;
using System.Threading.Tasks;

namespace FSL.VueTo.Core.Service
{
    public sealed class DatabaseApiClientService : IApiClientService
    {
        private readonly MyConfiguration _configuration;
        private readonly IFactoryService _factoryService;

        public DatabaseApiClientService(
            MyConfiguration configuration,
            IFactoryService factoryService)
        {
            _configuration = configuration;
            _factoryService = factoryService;
        }

        public async Task<IApiClientProvider> CreateInstanceAsync()
        {
            var instance = _factoryService
               .InstanceOf<IApiClientProvider>()
               .UseJsonContentType()
               .UseBaseUrl(_configuration.ApiUrl);

            return await Task.FromResult(instance);
        }
    }
}
