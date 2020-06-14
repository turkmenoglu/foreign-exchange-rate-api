using ForeignExchangeRate.API.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForeignExchangeRate.API.Installers
{
    public class ExceptionHandlingInstaller : IInstaller
    {
        public void Add(IServiceCollection services, IConfiguration configuration)
        {

        }

        public void Use(IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}