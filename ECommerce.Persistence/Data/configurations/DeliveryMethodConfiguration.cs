using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Data.configurations
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(x=>x.Price).HasPrecision(8,2);
            builder.Property(x => x.ShortName).HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.Property(x => x.DeliveryTime).HasMaxLength(50);
        }
    }
}
