using ForeignExchangeRate.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForeignExchangeRate.API.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void Add(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(ICachingService<>), typeof(CachingService<>));
            services.AddTransient(typeof(IPublicationDateService), typeof(PublicationDateService));
            services.AddTransient(typeof(IForeignExchangeRatesService), typeof(ForeignExchangeRatesService));
        }

        public void Use(IApplicationBuilder app, IConfiguration configuration)
        {

        }
    }
}
