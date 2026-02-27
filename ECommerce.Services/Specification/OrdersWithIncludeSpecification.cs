using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Entities.OrderModule;

namespace ECommerce.Services.Specification
{
    internal class OrdersWithIncludeSpecification : BaseSpecification<Order, Guid>
    {
        public OrdersWithIncludeSpecification(string UserEmail) : base(order => order.UserEmail == UserEmail)
        {  
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
        }
    }
}
