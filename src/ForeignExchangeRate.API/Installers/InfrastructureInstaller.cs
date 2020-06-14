using ForeignExchangeRate.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ForeignExchangeRate.API.Installers
{
    public class InfrastructureInstaller : IInstaller
    {
        public void Add(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("ForeignExchangeDB"));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
        }

        public void Use(IApplicationBuilder app, IConfiguration configuration)
        {
            
        }
    }
}
