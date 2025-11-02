
using DomainLayer.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PersistenceLayer;
using PersistenceLayer.Data;
using PersistenceLayer.Repositories;
using ServiceAbstractionLayer;
using ServiceLayer;
using ServiceLayer.MappingProfiles;
using System.Threading.Tasks;

namespace TalabatDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 
            #region Add services to the DI container.
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnecions"));

            });
            #region Register User_defined Services
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            #region Mapping Regiser
            // builder.Services.AddAutoMapper(p => p.AddProfile(new ProductProfile()));
            // builder.Services.AddAutoMapper(p => p.AddProfile(new OrderProfile()));
            // builder.Services.AddAutoMapper(p => p.AddProfiles(new ProductProfile()));

            #endregion

            builder.Services.AddAutoMapper((x) => {  },typeof(ServiceLayerAssemblyReference).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();


            #endregion
            #endregion
            var app = builder.Build();

            #region Data Seeding

            using var scope= app.Services.CreateScope();
           var seedObj= scope.ServiceProvider.GetRequiredService<IDataSeeding>();
           await seedObj.DataSeedAsync();
            #endregion

            #region Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                
                
            }

            app.UseHttpsRedirection();

           // app.UseAuthorization();

            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
            #endregion

        }
    }
}
