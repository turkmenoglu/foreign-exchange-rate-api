using ForeignExchangeRate.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForeignExchangeRate.Infrastructure
{
    public interface IForeignExchangeRateRepository: IRepository<ForeignExchangeRateModel>
    {
        Task BulkSaveAsync(IList<ForeignExchangeRateModel> foreignExchangeRates, IList<ForeignExchangeRateModel> foreignExchangeRatesToDelete);
    }
}