using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Shared;

namespace ECommerce.Services.Specification
{
    internal class ProductWithTypeAndBrandSpecification : BaseSpecification<Product, int>
    {
        public ProductWithTypeAndBrandSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        public ProductWithTypeAndBrandSpecification(ProductQueryParams queryParams) :
            base(ProductSpecificationHelper.GetProductCriteria(queryParams))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            switch (queryParams.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddSortAsc(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddSortDesc(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddSortAsc(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddSortDesc(p => p.Price);
                    break;
                default:
                    AddSortAsc(p => p.Id);
                    break;
            }
            ApplyPagination(queryParams.PageIndex, queryParams.PageSize);
        }
    }
}
