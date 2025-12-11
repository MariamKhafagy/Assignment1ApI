using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using TalabatDemo.Factories;

namespace TalabatDemo.Extentions
{
    public  static class ServiceRegisteration
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {
          
            Services.AddOpenApi();

            return Services;
        
        }

        public static IServiceCollection AddWebApplicationServices(this IServiceCollection Services)
        {

            Services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorResponse;


            });
            return Services;

        }
    }
}
