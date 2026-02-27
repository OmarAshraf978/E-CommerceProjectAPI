using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Domain.Contracts.IRepository;
using ECommerce.Domain.Entities.BasketModule;
using ECommerce.Services.Exceptions;
using ECommerce.Services_Abstraction.IServices;
using ECommerce.Shared.Dtos.BasketDtos;

namespace ECommerce.Services.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto)
        {
            var basket = _mapper.Map<CustomerBasket>(basketDto);
            var createdOrUpdatedBasket = await _basketRepository.CreateOrUpdateBasketAsync(basket);
            return _mapper.Map<BasketDto>(createdOrUpdatedBasket);
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        => await _basketRepository.DeleteBasketAsync(basketId);

        public async Task<BasketDto> GetBasketAsync(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket is null)
                throw new BasketNotFoundException(basketId);
            return _mapper.Map<BasketDto>(basket!);
        }
    }
}
