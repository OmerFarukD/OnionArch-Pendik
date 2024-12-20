using ECommerce.Application.Services.Infrastructure;
using ECommerce.Infrastructure.Cloudinary;
using ECommerce.Infrastructure.CloudinaryServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection service,IConfiguration configuration)
    {
        service.AddScoped<ICloudinaryService, CloudinaryService>();
        service.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
        return service;

    }
}