using Microsoft.Extensions.DependencyInjection;
using RekognitionService.Application.Interfaces;
using RekognitionService.Application.Services;

namespace RekognitionService.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}