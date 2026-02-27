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
    #region ProductService
    public interface IProductService
    {
        Task<PaginationResult<ProductDto>> GetAllProductAsync(ProductQueryParams queryParams);
        Task<Result<ProductDto>> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
    }
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandDto>>(brands);
        }
        public async Task<PaginationResult<ProductDto>> GetAllProductAsync(ProductQueryParams queryParams)
        {
            var spec = new ProductWithTypeAndBrandSpecification(queryParams);
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllWithSpecificationAsync(spec);
            var dataToReturn = _mapper.Map<IEnumerable<ProductDto>>(products);
            var countOfReturnedData = dataToReturn.Count();
            var specCount = new ProductCountSpecifications(queryParams);
            var countOfTotalData = await _unitOfWork.GetRepository<Product, int>().CountAsync(specCount);
            return new PaginationResult<ProductDto>(queryParams.PageIndex, countOfReturnedData, countOfTotalData, dataToReturn);
        }
        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeDto>>(types);
        }
        public async Task<Result<ProductDto>> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithTypeAndBrandSpecification(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdWithSpecificationAsync(spec);
            if (product is null)
                return Error.NotFound("Product.NotFound", $"Product with id {id} was not found.");
            return _mapper.Map<ProductDto>(product);
        }
    }
    #endregion
}
