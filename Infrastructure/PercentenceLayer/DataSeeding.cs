using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PersistenceLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PersistenceLayer
{
    public class DataSeeding :IDataSeeding
    {

        private readonly StoreDbContext _storeDbContext;

        public DataSeeding(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        public async Task DataSeedAsync()
        {
            
            
            
                 //Production
                if ((await _storeDbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                   await _storeDbContext.Database.MigrateAsync();
                }

                if (!_storeDbContext.ProductBrands.Any())
                {
                   // var productBrandsData = await File.ReadAllTextAsync(@"..\Infrastructure\PercentenceLayer\Data\DataSeed\brands.json");
                    var productBrandsData =  File.OpenRead(@"..\Infrastructure\PercentenceLayer\Data\DataSeed\brands.json");

                    //Convert String To C# Object
                    var brands =   await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandsData);

                    if (brands is not null && brands.Any())
                    {
                      await  _storeDbContext.AddRangeAsync(brands);
                    }
                }

                if (!_storeDbContext.ProductTypes.Any())
                {
                    var productTypessData = File.OpenRead(@"..\Infrastructure\PercentenceLayer\Data\DataSeed\types.json");
                    //Convert String To C# Object
                    var types =  await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypessData);

                    if (types is not null && types.Any())
                    {
                       await _storeDbContext.AddRangeAsync(types);
                    }
                }

                if (!_storeDbContext.Products.Any())
                {
                    var productsData = File.OpenRead(@"..\Infrastructure\PercentenceLayer\Data\DataSeed\products.json");
                    //Convert String To C# Object
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productsData);

                    if (products is not null && products.Any())
                    {
                       await _storeDbContext.AddRangeAsync(products);
                    }
                }
            try
            {
                await _storeDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(" SaveChanges failed:");
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw;
            }
            try
            {
                await _storeDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ SaveChanges failed:");
                Console.WriteLine(ex.Message);
                Console.WriteLine("---- INNER EXCEPTION ----");
                Console.WriteLine(ex.InnerException?.Message);
                Console.WriteLine("---- STACK TRACE ----");
                Console.WriteLine(ex.StackTrace);
                throw;
            }



        }


    }

}
