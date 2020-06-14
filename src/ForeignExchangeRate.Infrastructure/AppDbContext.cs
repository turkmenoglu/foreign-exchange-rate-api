using ForeignExchangeRate.Model;
using Microsoft.EntityFrameworkCore;

namespace ForeignExchangeRate.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<ForeignExchangeRateModel> ForeignExchangeRateModels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ForeignExchangeRateModel>().ToTable(AppConstants.TableNameForeignExchangeRates);
            builder.Entity<ForeignExchangeRateModel>().HasKey(p => p.Id);
            builder.Entity<ForeignExchangeRateModel>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<ForeignExchangeRateModel>().Property(p => p.Date).IsRequired().HasMaxLength(AppConstants.ColumnMaxLengthDate);
            builder.Entity<ForeignExchangeRateModel>().Property(p => p.BaseCurrency).IsRequired().HasMaxLength(AppConstants.ColumnMaxLengthBaseCurrency);
            builder.Entity<ForeignExchangeRateModel>().Property(p => p.TargetCurrency).IsRequired().HasMaxLength(AppConstants.ColumnMaxLengthTargetCurrency);
            builder.Entity<ForeignExchangeRateModel>().Property(p => p.InverseRate).IsRequired();
            builder.Entity<ForeignExchangeRateModel>().Property(p => p.PublicationDate).IsRequired().HasMaxLength(AppConstants.ColumnMaxLengthPublicationDate);
        }
    }
}