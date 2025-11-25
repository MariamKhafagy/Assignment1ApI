using DomainLayer.Contracts;
using DomainLayer.Models.BasketModels;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PersistenceLayer.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var jsonBsket=JsonSerializer.Serialize(basket);
           var  isCreatedOrUbdared= await _database.StringSetAsync(basket.Id ,jsonBsket, TimeToLive?? TimeSpan.FromDays(30));

            if (isCreatedOrUbdared) { return basket; }
             else return null;
        }

        public async Task<bool> DeleteBasketAsync(string Key)
        =>await _database.KeyDeleteAsync(Key);

        public async Task<CustomerBasket?> GetBasketASync(string Key)
        {
            var basket=await _database.StringGetAsync(Key);

            if (basket.IsNullOrEmpty) return null;
            else return JsonSerializer.Deserialize<CustomerBasket?>(basket!);
        }
    }
}
