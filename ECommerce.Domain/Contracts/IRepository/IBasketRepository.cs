using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Entities.BasketModule;

namespace ECommerce.Domain.Contracts.IRepository
{
    public interface IBasketRepository
    {
        public Task<CustomerBasket?> GetBasketAsync(string basketId);
        public Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan TimeToLive = default);
        public Task<bool> DeleteBasketAsync(string basketId);
    }
}
