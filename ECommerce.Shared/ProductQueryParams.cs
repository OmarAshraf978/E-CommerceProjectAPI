using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared
{
    public class ProductQueryParams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? ProductName { get; set; }
        public ProductSortingOptions Sort { get; set; }
        private int _pageIndex = 1;
        public int PageIndex
        {
            get { return _pageIndex; }
            set
            {
                _pageIndex = (value <=0) ? 1 : value;
            }
        }
        private const int defaultPageSize = 5;
        private const int maxPageSize = 10;
        private int _pageSize = defaultPageSize;
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (value > maxPageSize)
                {
                    _pageSize = maxPageSize;
                }
                else if (value <= 0)
                {
                    _pageSize = defaultPageSize;
                }
                else
                {
                    _pageSize = value;
                }
            }
        }
    }
}
