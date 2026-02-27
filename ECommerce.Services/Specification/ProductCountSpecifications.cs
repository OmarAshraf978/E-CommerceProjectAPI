using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Shared;

namespace ECommerce.Services.Specification
{
    internal class ProductCountSpecifications : BaseSpecification<Product, int>
    {
        public ProductCountSpecifications(ProductQueryParams queryParams) :
            base(ProductSpecificationHelper.GetProductCriteria(queryParams))
        {

        }
    }
}
