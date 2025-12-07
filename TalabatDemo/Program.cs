
using Azure;
using DomainLayer.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens.Experimental;
using PersistenceLayer;
using PersistenceLayer.Data;
using PersistenceLayer.Repositories;
using ServiceAbstractionLayer;
using ServiceLayer;
using ServiceLayer.MappingProfiles;
using Shared.Error_Models;
using System.Threading.Tasks;
using TalabatDemo.CustomMiddleWares;
using TalabatDemo.Extentions;
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
            builder.Services.AddSwaggerServices();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            #region Register User_defined Services
            //
            builder.Services.AddApplicationServices();
            builder.Services.AddInfraStructureService(builder.Configuration);
          builder.Services.AddWebApplicationServices();

            #endregion
            #endregion
            var app = builder.Build();
           await app.SeedDatabaseAsync();
            

            #region Configure the HTTP request pipeline.

            #region This code is Changed by try and catch in CustomExclass
            //app.Use(async (RequestContext, NextMiddleWare) =>
            //{
            //    Console.WriteLine("Request Under Processing");
            //    await NextMiddleWare.Invoke();
            //    Console.WriteLine("waiting Responce");


            //});

            #endregion  

            app.UseCustomExceptionMiddleware();

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
