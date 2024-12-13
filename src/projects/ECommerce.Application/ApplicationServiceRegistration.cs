using Core.Application.Pipelines.Validation;
using Core.CrossCuttingConcerns.Serilog.Loggers;
using ECommerce.Application.Features.Categories.Rules;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Login;
using Core.Application.Pipelines.Performance;
using ECommerce.Application.Features.Auth.Rules;
using ECommerce.Application.Services.UserServices;
using MediatR;

namespace ECommerce.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServiceDependencies(this IServiceCollection services)
    {

        services.AddScoped<UserBusinessRules>();
        services.AddScoped<CategoryBusinessRules>();
        services.AddScoped<IUserService, UserService>();
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddMediatR(con => {
            con.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            con.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            con.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            con.AddOpenBehavior(typeof(LoginBehavior<,>));
        } );

        return services;
    }

}
