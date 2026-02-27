using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Shared.Dtos.ProductDtos;

namespace ECommerce.Services.MappingProfile
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<Product, ProductDto>()
                     .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                     .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Name))
                     .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductPictureUrlResolver>());
            CreateMap<ProductType, TypeDto>();
        }
    }
}
