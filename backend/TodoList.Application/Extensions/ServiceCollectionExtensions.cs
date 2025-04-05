using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace TodoList.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediator();
        services.AddValidators();
    }
    
    private static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }

    private static void AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }        
}