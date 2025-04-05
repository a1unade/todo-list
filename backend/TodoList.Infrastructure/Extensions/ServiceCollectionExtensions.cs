using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Application.Interfaces.Services;
using TodoList.Infrastructure.Services;

namespace TodoList.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddServices();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<IMediator, Mediator>()
            .AddTransient<IEmailService, EmailService>();
    }
}