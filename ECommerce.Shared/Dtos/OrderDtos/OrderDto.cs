using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Dtos.OrderDtos
{
    public record OrderDto
    (
        string BasketId,
        OrderAddressDto Address,
        int DeliveryMethodId
    );
}
