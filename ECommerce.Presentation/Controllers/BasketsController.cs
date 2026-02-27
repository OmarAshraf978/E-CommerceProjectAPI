using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Services_Abstraction.IServices;
using ECommerce.Shared.Dtos.BasketDtos;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Presentation.Controllers
{
    public class BasketsController : ApiBaseController
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string basketId)
        {
            var basket = await _basketService.GetBasketAsync(basketId);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basketDto)
        {
            var basket = await _basketService.CreateOrUpdateBasketAsync(basketDto);
            return Ok(basket);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string basketId)
        {
            var result = await _basketService.DeleteBasketAsync(basketId);
            return Ok(result);
        }
    }
}
