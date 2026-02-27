using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Dtos.OrderDtos
{
    public class OrderToReturnDto
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; } = default!;
        public ICollection<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
        public OrderAddressDto Address { get; set; } = default!;
        public string DeliveryMethod { get; set; } = default!;
        public string OrdersStatus { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
    }

}
