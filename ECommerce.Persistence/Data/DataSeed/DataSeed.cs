using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ECommerce.Domain.Contracts.DataSeed;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.OrderModule;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.Data.DataSeed
{
    public class DataSeed : IDataSeed
    {
        private readonly StoreDbContext _context;

        public DataSeed(StoreDbContext context)
        {
            _context = context;
        }
        public async Task SeedDataAsync()
        {
            try
            {
                var HasBrand = await _context.ProductBrands.AnyAsync();
                var HasType = await _context.ProductTypes.AnyAsync();
                var HasProduct = await _context.Products.AnyAsync();
                var HasDeliveryMethod = await _context.Set<DeliveryMethod>().AnyAsync();
                if (HasBrand && HasType && HasProduct && HasDeliveryMethod)
                {
                    return;
                }
                if (!HasBrand)
                {
                    await SeedDataFromJsonAsync<ProductBrand, int>("brands.json", _context.ProductBrands);
                }
                if (!HasType)
                {
                    await SeedDataFromJsonAsync<ProductType, int>("types.json", _context.ProductTypes);
                }
                await _context.SaveChangesAsync();
                if (!HasProduct)
                {
                    await SeedDataFromJsonAsync<Product, int>("products.json", _context.Products);
                }
                if (!HasDeliveryMethod)
                {
                    await SeedDataFromJsonAsync<DeliveryMethod, int>("delivery.json", _context.Set<DeliveryMethod>());
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Seeding Data : {ex}");
            }

        }
        private async Task SeedDataFromJsonAsync<T, TKey>(string FileName, DbSet<T> DbSet) where T : BaseEntity<TKey>
        {
            var filePath = @"../ECommerce.Persistence/Data/DataSeed/JSONFiles/" + FileName;
            if(!File.Exists(filePath)) throw new FileNotFoundException($"File {FileName} is not exists");
            try
            {
                using var dataStream = File.OpenRead(filePath);
                var Data = await JsonSerializer.DeserializeAsync<List<T>>(dataStream, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
                if (Data is not null)
                {
                    await DbSet.AddRangeAsync(Data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Reading JSON File : {ex}");
            }
        }
    }
}
