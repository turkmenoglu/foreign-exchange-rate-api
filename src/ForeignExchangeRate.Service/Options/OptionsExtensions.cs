using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForeignExchangeRate.Service.Options
{
    public static class OptionsExtensions
    {
        private const string RequestResponseLoggingMiddlewareOptionKey = "RequestResponseLoggingMiddleware";
        private const string ForeignExchangeRateOptionKey = "ForeignExchangeRate";

        public static IServiceCollection ConfigureOption(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.ConfigureOption<RequestResponseLoggingMiddlewareOption>(configuration, RequestResponseLoggingMiddlewareOptionKey);
            services.ConfigureOption<ForeignExchangeRateOption>(configuration, ForeignExchangeRateOptionKey);
            return services;
        }

        public static void ConfigureOption<T>(this IServiceCollection services, IConfiguration configuration, string key)
            where T : OptionBase
        {
            services.Configure<T>(x => configuration.GetSection(key).Bind(x));
        }
    }
}
