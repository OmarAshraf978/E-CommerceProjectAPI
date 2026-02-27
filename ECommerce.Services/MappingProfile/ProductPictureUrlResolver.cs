using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Shared.Dtos.ProductDtos;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Services.MappingProfile
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;
        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }
            if(source.PictureUrl.StartsWith("http"))
            {
                return source.PictureUrl;
            }
            var BaseUrl = _configuration.GetSection("URLs")["BaseUrl"];
            if(string.IsNullOrEmpty(BaseUrl))
            {
                return string.Empty;
            }
            var pictureUrl = $"{BaseUrl}{source.PictureUrl}";
            return pictureUrl;
        }
    }
}
