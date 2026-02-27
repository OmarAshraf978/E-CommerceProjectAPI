using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Data.configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> P)
        {
            P.Property(p => p.Name).HasMaxLength(100);
            P.Property(p => p.Description).HasMaxLength(500);
            P.Property(p => p.PictureUrl).HasMaxLength(200);
            P.Property(p => p.Price).HasPrecision(18, 2);
            P.HasOne(p => p.ProductBrand)
             .WithMany()
             .HasForeignKey(p => p.BrandId)
             .OnDelete(DeleteBehavior.NoAction);
            P.HasOne(p => p.ProductType)
             .WithMany()
             .HasForeignKey(p => p.TypeId)
             .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
