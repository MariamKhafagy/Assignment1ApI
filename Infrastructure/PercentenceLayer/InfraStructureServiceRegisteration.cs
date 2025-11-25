using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersistenceLayer.Data;
using PersistenceLayer.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLayer
{
    public static class InfraStructureServiceRegisteration
    {
        public static IServiceCollection AddInfraStructureService(this IServiceCollection Services, IConfiguration _configuration)
        {
            Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConncetion"));

            });
            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();

            Services.AddScoped<IBasketRepository, BasketRepository>();

            Services.AddSingleton<IConnectionMultiplexer>( (_) =>
            {
                return  ConnectionMultiplexer.Connect(_configuration.GetConnectionString("RedisConnectionString"));


            });

            return Services;
          
        }
    }
}
