using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Shared.Dtos.OrderDtos;
using ECommerce.Shared.ResultPattern;

namespace ECommerce.Services_Abstraction.IServices
{
    public interface IOrderService
    {
        public Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string Email);
        public Task<Result<List<OrderToReturnDto>>> GetAllOrderAsync(string Email);
        public Task<Result<OrderToReturnDto>> GetOrderByIdAsync(Guid Id);
        public Task<Result<List<DeliveryMethodDto>>> GetAllDeliveryMethodsAsync();
    }
}
