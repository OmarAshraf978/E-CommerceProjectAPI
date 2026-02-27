using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services_Abstraction.IServices
{
    public interface ICacheService
    {
        public Task<string?> GetAsync(string cachekey);
        public Task SetAsync(string cacheKey, object cacheValue, TimeSpan TimeToLive);
    }
}
