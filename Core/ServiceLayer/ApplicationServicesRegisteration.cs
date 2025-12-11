using Microsoft.Extensions.DependencyInjection;
using ServiceAbstractionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public  static class ApplicationServicesRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper((x) => { }, typeof(ServiceLayerAssemblyReference).Assembly);
            Services.AddScoped<IServiceManager, ServiceManager>();

            return Services;

        }
    }
}
