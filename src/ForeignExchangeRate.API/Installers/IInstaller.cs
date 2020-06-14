using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForeignExchangeRate.API.Installers
{
    public interface IInstaller
    {
        void Add(IServiceCollection services, IConfiguration configuration);
        void Use(IApplicationBuilder app, IConfiguration configuration);
    }
}