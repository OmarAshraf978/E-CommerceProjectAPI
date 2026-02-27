using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Presentation.Attributes;
using ECommerce.Services_Abstraction.IServices;
using ECommerce.Shared;
using ECommerce.Shared.Dtos.ProductDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Presentation.Controllers
{
    public class ProductsController : ApiBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [HttpGet]
        [RedisCache]
        public async Task<ActionResult<PaginationResult<ProductDto>>> GetAllProduct([FromQuery] ProductQueryParams queryParams)
        {
            var products = await _productService.GetAllProductAsync(queryParams);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var Result = await _productService.GetProductByIdAsync(id);
            return HandleResult<ProductDto>(Result);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands = await _productService.GetAllBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var types = await _productService.GetAllTypesAsync();
            return Ok(types);
        }
    }
}
