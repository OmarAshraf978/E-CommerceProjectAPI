using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Shared;

namespace ECommerce.Services.Specification
{
    internal static class ProductSpecificationHelper
    {
        public static Expression<Func<Product, bool>> GetProductCriteria(ProductQueryParams queryParams)
        {
            return p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId.Value)
                   && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId.Value)
                   && (string.IsNullOrEmpty(queryParams.ProductName) || p.Name.ToLower().Contains(queryParams.ProductName.ToLower()));
        }
    }
}
