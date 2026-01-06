using n8nAPI.Common.Base;
using n8nAPI.Common.Configurations;
using System.Reflection;

namespace n8nAPI.Common.Extensions;

public static class ServiceExtensions
{
    public static IEndpointRouteBuilder RegisterEndpoints(
        this IEndpointRouteBuilder routeBuilder,
        Action<EndpointConfiguration> configuration = default!)
    {
        EndpointConfiguration config = new();
        configuration?.Invoke(config);

        List<Type> types = GetEndpointTypes();

        foreach (Type type in types)
            CreateEndpoint(type, routeBuilder.MapGroup(config.GetPrefix()));

        return routeBuilder;
    }

    public static IServiceCollection RegisterServices(
        this IServiceCollection services)
    {
        return services;
    }

    private static List<Type> GetEndpointTypes()
    {
        var assembly = Assembly.GetAssembly(typeof(ServiceExtensions))
            ?? throw new InvalidOperationException("Cannot find assembly");

        return [.. assembly.GetTypes()
            .Where(t => t.IsClass
                     && !t.IsAbstract
                     && typeof(EndpointBase).IsAssignableFrom(t))];
    }

    private static void CreateEndpoint(Type type, IEndpointRouteBuilder globalRoute)
    {
        if (Activator.CreateInstance(type) is EndpointBase ep)
            ep.RegisterEndpoint(globalRoute);
    }
}
