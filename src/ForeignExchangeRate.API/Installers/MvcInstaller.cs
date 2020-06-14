using FluentValidation.AspNetCore;
using ForeignExchangeRate.API.Filters;
using ForeignExchangeRate.Service.Options;
using ForeignExchangeRate.DataPolicy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForeignExchangeRate.API.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void Add(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;                    
                    options.Filters.Add<ValidationFilter>();
                })
                .ConfigureApiBehaviorOptions(options => {
                    options.SuppressModelStateInvalidFilter = true;
                })
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<IDataPolicy>();
                });

            services.ConfigureOption(configuration);
        }

        public void Use(IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseMvc();
        }
    }
}
