using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.Data.configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.Subtotal).HasPrecision(8, 2);
            builder.OwnsOne(x => x.Address, OEntity =>
            {
                OEntity.Property(x => x.Street).HasMaxLength(50);
                OEntity.Property(x => x.City).HasMaxLength(50);
                OEntity.Property(x => x.Country).HasMaxLength(50);
                OEntity.Property(x => x.FirstName).HasMaxLength(50);
                OEntity.Property(x => x.LastName).HasMaxLength(50);
            });
        }
    }
}
