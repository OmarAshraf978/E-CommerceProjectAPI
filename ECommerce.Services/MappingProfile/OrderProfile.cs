using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Domain.Entities.OrderModule;
using ECommerce.Shared.Dtos.OrderDtos;

namespace ECommerce.Services.MappingProfile
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderAddress, OrderAddressDto>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemPictureUrlResolver>());
            CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();
        }
    }
}
