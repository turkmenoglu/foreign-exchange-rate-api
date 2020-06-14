using ForeignExchangeRate.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForeignExchangeRate.Infrastructure
{
    public class EntityConfiguration : IEntityTypeConfiguration<ForeignExchangeRateModel>
    {
        public void Configure(EntityTypeBuilder<ForeignExchangeRateModel> builder)
        {
            builder.HasKey(_ => _.Id);
        }
    }
}
