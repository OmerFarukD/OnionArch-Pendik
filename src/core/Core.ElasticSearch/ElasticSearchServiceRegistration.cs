using Core.ElasticSearch.Services.Abstracts;
using Core.ElasticSearch.Services.Concretes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace Core.ElasticSearch;

public static class ElasticSearchServiceRegistration
{
    public static IServiceCollection AddElasticSearch(this IServiceCollection services,
        IConfiguration configuration
        )
    {
        services.AddScoped<IElasticSearchClientService, ElasticSearchClientService>();
        services.AddSingleton<IElasticClient>(sp =>
        {
            var settings = new ConnectionSettings(
                new Uri(configuration.GetConnectionString("ElasticSearch"))
            ).DefaultIndex("log-projects");

            
            return new ElasticClient(settings);
        });
        
        return services;
    }
}