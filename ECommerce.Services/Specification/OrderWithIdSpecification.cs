using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Entities.OrderModule;

namespace ECommerce.Services.Specification
{
    internal class OrderWithIdSpecification : BaseSpecification<Order, Guid>
    {
        public OrderWithIdSpecification(Guid Id) : base(order => order.Id == Id)
        { }
    }
}
