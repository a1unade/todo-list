using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Application.Interfaces.Services;
using TodoList.Infrastructure.Behaviours;
using TodoList.Infrastructure.Services;

namespace TodoList.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddServices();
        services.AddMemoryCache();
        services.AddPipelineBehaviours();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<IMediator, Mediator>()
            .AddScoped<IJwtGenerator, JwtGenerator>()
            .AddTransient<IEmailService, EmailService>();
    }

    private static void AddPipelineBehaviours(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
    }
}