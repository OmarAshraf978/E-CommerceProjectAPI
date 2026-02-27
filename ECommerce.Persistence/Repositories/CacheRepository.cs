using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Contracts.IRepository;
using StackExchange.Redis;

namespace ECommerce.Persistence.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _database;
        public CacheRepository(IConnectionMultiplexer connection) 
        { 
            _database = connection.GetDatabase();
        }

        public async Task<string?> GetAsync(string cachekey)
        {
            var cacheValue = await _database.StringGetAsync(cachekey);
            return cacheValue.IsNullOrEmpty ? null : cacheValue.ToString();
        }

        public async Task SetAsync(string cacheKey, string cacheValue, TimeSpan TimeToLive)
        {
            await _database.StringSetAsync(cacheKey, cacheValue, TimeToLive);
        }
    }
}
