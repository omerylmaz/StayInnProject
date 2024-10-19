using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BuildingBlocks.Endpoints;

public static class AddCustomEndpointConfiguration
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors = assembly.DefinedTypes.Where(type => type is { IsAbstract: false, IsInterface: false } &&
        type.IsAssignableTo(typeof(IEndpointModule)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpointModule), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }


    public static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IEndpointModule> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpointModule>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (var enpoint in endpoints) 
        {
            enpoint.AddRoutes(app);
        }

        return app;
    }
}