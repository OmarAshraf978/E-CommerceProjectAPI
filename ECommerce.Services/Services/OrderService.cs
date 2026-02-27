using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Domain.Contracts.IRepository;
using ECommerce.Domain.Contracts.IUnitOfWork;
using ECommerce.Domain.Contracts.Specification;
using ECommerce.Domain.Entities.IdentityModule;
using ECommerce.Domain.Entities.OrderModule;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Services.Specification;
using ECommerce.Services_Abstraction.IServices;
using ECommerce.Shared.Dtos.OrderDtos;
using ECommerce.Shared.ResultPattern;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IMapper mapper, IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string Email)
        {
            var OrderAddress = _mapper.Map<OrderAddress>(orderDto.Address);
            var Basket = await _basketRepository.GetBasketAsync(orderDto.BasketId);
            if (Basket is null) return Error.NotFound("BASKET.NotFound", $"Basket With Id {orderDto.BasketId} Is Not Found");
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (var item in Basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id);
                if (product is null) return Error.NotFound("Product.NotFound", $"Product With Id {item.Id} Is Not Found");
                var OrderItem = new OrderItem()
                {
                    Product = new ProductItemOrder()
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        PictureUrl = product.PictureUrl
                    },
                    Price = product.Price,
                    Quantity = item.Quantity
                };
                orderItems.Add(OrderItem);
            }
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId);
            if (DeliveryMethod is null) return Error.NotFound("DeliveryMethod.NotFound", $"Delivery Method With Id {orderDto.DeliveryMethodId} Is Not Found");
            var Subtotal = orderItems.Sum(item => item.Price * item.Quantity);
            var Order = new Order()
            {
                UserEmail = Email,
                OrderItems = orderItems,
                Address = OrderAddress,
                DeliveryMethod = DeliveryMethod,
                Subtotal = Subtotal
            };
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(Order);
            var Result = await _unitOfWork.SaveChangesAsync();
            if (Result == 0) return Error.Failure("Order.Failed", "An Error Occurred While Creating The Order");
            return _mapper.Map<OrderToReturnDto>(Order);
        }

        public async Task<Result<List<DeliveryMethodDto>>> GetAllDeliveryMethodsAsync()
        {
            var DeliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            var DeliveryMethodsToReturn = _mapper.Map<List<DeliveryMethodDto>>(DeliveryMethods);
            return DeliveryMethodsToReturn;
        }

        public async Task<Result<List<OrderToReturnDto>>> GetAllOrderAsync(string Email)
        {
            if (Email is null) return Error.NotFound("Orders.NotFound", $"Orders With Email: {Email} Not Found");
            var spec = new OrdersWithIncludeSpecification(Email);
            var Orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllWithSpecificationAsync(spec);
            var OrdersToReturn = _mapper.Map<List<OrderToReturnDto>>(Orders);
            return OrdersToReturn;
        }

        public async Task<Result<OrderToReturnDto>> GetOrderByIdAsync(Guid Id)
        {
            var spec = new OrderWithIdSpecification(Id);
            var Order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdWithSpecificationAsync(spec);
            if (Order is null) return Error.NotFound("Order.NotFound", $"Order With Id {Id} Is Not Found");
            var OrderToReturn = _mapper.Map<OrderToReturnDto>(Order);
            return OrderToReturn;
        }


    }
}
