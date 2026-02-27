using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Shared;
using ECommerce.Shared.Dtos.ProductDtos;
using ECommerce.Shared.ResultPattern;

namespace ECommerce.Services_Abstraction.IServices
{
    public interface IProductService
    {
        Task<PaginationResult<ProductDto>> GetAllProductAsync(ProductQueryParams queryParams);
        Task<Result<ProductDto>> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
    }
}
