using Amazon.DynamoDBv2;
using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using Microsoft.Extensions.DependencyInjection;
using RekognitionService.Core.Interfaces;
using RekognitionService.Infrastructure.Repositories;

namespace RekognitionService.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddAWSServices(this IServiceCollection services, AWSOptions options)
        {
            services.AddAWSService<IAmazonS3>(options);
            services.AddAWSService<IAmazonDynamoDB>();

            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}