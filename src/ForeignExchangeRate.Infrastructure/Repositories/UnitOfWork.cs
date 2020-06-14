using System;
using System.Threading.Tasks;

namespace ForeignExchangeRate.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public IForeignExchangeRateRepository ForeignExchangeRate { get; }
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            ForeignExchangeRate = new ForeignExchangeRateRepository(_appDbContext);
        }

        public async Task CompleteAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
    public interface IUnitOfWork : IDisposable
    {
        IForeignExchangeRateRepository ForeignExchangeRate { get; }
        Task CompleteAsync();
    }
}
