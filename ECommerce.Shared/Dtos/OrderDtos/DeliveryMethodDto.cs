using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Dtos.OrderDtos
{
    public record DeliveryMethodDto
    (
        string ShortName,
        string Description,
        string DeliveryTime,
        decimal Price
    );
}
