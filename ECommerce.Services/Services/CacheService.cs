using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ECommerce.Domain.Contracts.IRepository;
using ECommerce.Services_Abstraction.IServices;

namespace ECommerce.Services.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _repository;

        public CacheService(ICacheRepository repository)
        {
            _repository = repository;
        }
        public async Task<string?> GetAsync(string cachekey)
        {
            return await _repository.GetAsync(cachekey);
        }

        public async Task SetAsync(string cacheKey, object cacheValue, TimeSpan TimeToLive)
        {
            var value = JsonSerializer.Serialize(cacheValue);
            await _repository.SetAsync(cacheKey, value, TimeToLive);
        }
    }
}
