using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Services_Abstraction.IServices;
using ECommerce.Shared.Dtos.OrderDtos;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Presentation.Controllers
{
    public class OrderController : ApiBaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("CreateOrder")]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto, string email)
        {
            var result = await _orderService.CreateOrderAsync(orderDto, email);
            return HandleResult(result);
        }

        [HttpGet("GetAllOrders")]
        public async Task<ActionResult<List<OrderToReturnDto>>> GetAllOrders(string email)
        {
            var result = await _orderService.GetAllOrderAsync(email);
            return HandleResult(result);
        }

        [HttpGet("GetOrderById")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id)
        {
            var result = await _orderService.GetOrderByIdAsync(id);
            return HandleResult(result);
        }

        [HttpGet("GetAllDeliveryMethods")]
        public async Task<ActionResult<List<DeliveryMethodDto>>> GetAllDeliveryMethods()
        {
            var result = await _orderService.GetAllDeliveryMethodsAsync();
            return HandleResult(result);
        }
    }
}
