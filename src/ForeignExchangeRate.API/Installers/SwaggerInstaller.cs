using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace ForeignExchangeRate.API.Installers
{
    public class SwaggerInstaller : IInstaller
    {
        public void Add(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ForeignExchangeRate API",
                    Version = "v1",
                    Description = "RESTful API built with ASP.NET Core 3.1 to show foreign exchange rate infos in http://www.floatrates.com/daily/try.xml.",
                    Contact = new OpenApiContact
                    {
                        Name = "İsmail Türkmenoğlu",
                        Url = new Uri("https://github.com/turkmenoglu")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "",
                    },
                });

                cfg.EnableAnnotations();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                cfg.IncludeXmlComments(xmlPath);
            });
        }

        public void Use(IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger().UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ForeignExchangeRate API");
                options.DocumentTitle = "ForeignExchangeRate API";
            });
        }
    }
}
