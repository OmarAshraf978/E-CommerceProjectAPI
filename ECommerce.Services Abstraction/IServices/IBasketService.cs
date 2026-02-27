using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Shared.Dtos.BasketDtos;

namespace ECommerce.Services_Abstraction.IServices
{
    public interface IBasketService
    {
        public Task<BasketDto> GetBasketAsync(string basketId);
        public Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto);
        public Task<bool> DeleteBasketAsync(string basketId);
    }
}
