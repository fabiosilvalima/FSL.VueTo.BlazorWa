using FSL.Framework.Core.ApiClient.Provider;
using FSL.Framework.Core.ApiClient.Service;
using FSL.Framework.Core.Factory.Service;
using FSL.Framework.Core.Service;
using FSL.VueTo.Core.Models;
using FSL.VueTo.Core.Service;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FSL.VueTo.BlazorWa
{
    public class Startup
    {
        public void ConfigureServices(
            IServiceCollection services)
        {
            services.AddTransient<IApiClientProvider, HttpClientApiClientProvider>();
            services.AddSingleton<IFactoryService, DefaultFactoryService>();
            services.AddSingleton<IApiClientService, DatabaseApiClientService>();
            services.AddSingleton(new MyConfiguration()
            {
                ApiUrl = "http://localhost/fsl-database-api/api/"
            });
        }

        public void Configure(
            IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
