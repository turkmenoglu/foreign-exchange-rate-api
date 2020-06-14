using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace ForeignExchangeRate.Service.Services
{
    public interface ICachingService<T> where T : class
    {
        Task<T> GetOrCreateAsync(string key, Func<Task<T>> create, int durationInHour);
        Task SetAsync(string key, T value);
    }
    public class CachingService<T> : IService, ICachingService<T> where T : class
    {
        private readonly IMemoryCache _cache;
        public CachingService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetOrCreateAsync(string key, Func<Task<T>> create, int durationInHour)
        {
            return await _cache.GetOrCreateAsync(key, data => {
                data.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(durationInHour);
                return create();
            });
        }
        
        public async Task SetAsync(string key, T value)
        {
            await Task.Run(() =>
            {
                _cache.Set<T>(key, value);
            });
        }
    }
}
