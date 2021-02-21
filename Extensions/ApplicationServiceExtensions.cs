using API.Data;
using API.Helpers;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddScoped<INewYorkRepository, NewYorkRepository>();
            services.AddScoped<IBmkRepository, BmkRepository>();
            services.AddScoped<IGeneralRepository, GeneralRepository>();
            return services;
        }
    }
}