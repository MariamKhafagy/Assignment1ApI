
using Azure;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens.Experimental;
using PersistenceLayer;
using PersistenceLayer.Data;
using PersistenceLayer.Identity;
using PersistenceLayer.Repositories;
using ServiceAbstractionLayer;
using ServiceLayer;
using ServiceLayer.MappingProfiles;
using Shared.Error_Models;
using System.Threading.Tasks;
using TalabatDemo.CustomMiddleWares;
using TalabatDemo.Factories;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            //builder.Services.AddDbContext<StoreDbContext>(options =>
            //{
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnecions"));

            //});
            #region Register User_defined Services
           

            builder.Services.AddInfraStructureService(builder.Configuration);
      

            builder.Services.AddAutoMapper((x) => { }, typeof(ServiceLayerAssemblyReference).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();


            

            builder.Services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorResponse;
               

            });

            #endregion
            #endregion
            var app = builder.Build();

            #region Data Seeding

            using var scope= app.Services.CreateScope();
           var seedObj= scope.ServiceProvider.GetRequiredService<IDataSeeding>();
           await seedObj.DataSeedAsync();
            await seedObj.IdentityDataSeedAsync();

            #endregion

            #region Configure the HTTP request pipeline.

            #region This code is Changed by try and catch in CustomExclass
            //app.Use(async (RequestContext, NextMiddleWare) =>
            //{
            //    Console.WriteLine("Request Under Processing");
            //    await NextMiddleWare.Invoke();
            //    Console.WriteLine("waiting Responce");


            //});

            #endregion  

            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();

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
