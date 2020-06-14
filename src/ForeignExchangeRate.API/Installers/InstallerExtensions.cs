using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ForeignExchangeRate.API.Installers
{
    public static class InstallerExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var exportedTypes = typeof(Startup).Assembly.ExportedTypes;
            var installers = exportedTypes.Where(x =>
                                typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
            installers.ForEach(installer => installer.Add(services, configuration));
        }

        public static void UseServices(this IApplicationBuilder app, IConfiguration configuration)
        {
            var exportedTypes = typeof(Startup).Assembly.ExportedTypes;
            var installers = exportedTypes.Where(x =>
                                typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
            installers.ForEach(installer => installer.Use(app, configuration));
        }
    }
}
