using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.Data.configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(x => x.Price).HasPrecision(8, 2);
            builder.OwnsOne(x => x.Product, OEntity =>
            {
                OEntity.Property(x => x.ProductName).HasMaxLength(100);
                OEntity.Property(x => x.PictureUrl).HasMaxLength(200);
            });
        }
    }
}
