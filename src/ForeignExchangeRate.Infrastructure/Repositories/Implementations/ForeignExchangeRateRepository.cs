using ForeignExchangeRate.Library.Extensions;
using ForeignExchangeRate.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForeignExchangeRate.Infrastructure
{
    public class ForeignExchangeRateRepository : Repository<ForeignExchangeRateModel>, IForeignExchangeRateRepository
    {
        public ForeignExchangeRateRepository(AppDbContext context) : base(context)
        {
        }

        public async Task BulkSaveAsync(IList<ForeignExchangeRateModel> foreignExchangeRates, IList<ForeignExchangeRateModel> foreignExchangeRatesToDelete)
        {
            if (!ListExtensions.IsNullOrEmpty(foreignExchangeRatesToDelete))
            {
                await _context.BulkDeleteAsync(foreignExchangeRatesToDelete);
            }

            if (!ListExtensions.IsNullOrEmpty(foreignExchangeRates))
            {
                await _context.AddRangeAsync(foreignExchangeRates);
            }
        }
    }
}
