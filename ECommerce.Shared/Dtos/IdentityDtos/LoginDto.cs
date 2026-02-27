using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Dtos.IdentityDtos
{
    public record LoginDto
    (
        [EmailAddress]
        string Email,
        string Password
    );
}
