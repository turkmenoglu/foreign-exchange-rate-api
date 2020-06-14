using ForeignExchangeRate.Contract;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForeignExchangeRate.API.Installers
{
    public class ContractInstaller : IInstaller
    {
        public void Add(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(IRequestHandler));
        }

        public void Use(IApplicationBuilder app, IConfiguration configuration)
        {
            
        }
    }
}
