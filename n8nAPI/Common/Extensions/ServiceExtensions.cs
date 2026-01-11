using Microsoft.AspNetCore.RateLimiting;
using n8nAPI.Common.Configurations;
using n8nAPI.Common.Constants;
using n8nAPI.MinimalAPI.Base;
using System.Reflection;
using System.Threading.RateLimiting;

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

    public static IServiceCollection RegisterRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(cf =>
        {
            cf.OnRejected += OnRateLimiterRejected;

            cf.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
            {
                var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                return RateLimitPartition.GetTokenBucketLimiter(ip, _ =>
                    new TokenBucketRateLimiterOptions
                    {
                        TokenLimit = 100,
                        TokensPerPeriod = 25,
                        ReplenishmentPeriod = TimeSpan.FromSeconds(10),
                        AutoReplenishment = true,
                        QueueLimit = 0
                    });
            });

            cf.AddFixedWindowLimiter(RateLimiters.FixedWindowLimiter, options =>
            {
                options.PermitLimit = 2;
                options.Window = TimeSpan.FromMinutes(1);
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = 0;
            });

            cf.AddTokenBucketLimiter(RateLimiters.TokenBucketLimiter, options =>
            {
                options.TokenLimit = 100;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = 0;
                options.ReplenishmentPeriod = TimeSpan.FromSeconds(30);
                options.TokensPerPeriod = 50;
                options.AutoReplenishment = true;
            });

            cf.AddFixedWindowLimiter(RateLimiters.TestLimiter, options =>
            {
                options.PermitLimit = 2;
                options.Window = TimeSpan.FromMinutes(1);
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = 0;
            });
        });

        return services;
    }

    private static List<Type> GetEndpointTypes()
    {
        var assembly = Assembly.GetAssembly(typeof(ServiceExtensions))
            ?? throw new InvalidOperationException("Cannot find assembly");

        return [.. assembly.GetTypes()
            .Where(t => t.IsClass
                     && !t.IsAbstract
                     && !t.Name.EndsWith("Group") // TODO: Better way to exclude group classes
                     && typeof(EndpointBase).IsAssignableFrom(t))];
    }

    private static void CreateEndpoint(Type type, IEndpointRouteBuilder globalRoute)
    {
        if (Activator.CreateInstance(type) is EndpointBase ep)
            ep.RegisterEndpoint(globalRoute);
    }

    private static async ValueTask OnRateLimiterRejected(OnRejectedContext context, CancellationToken token)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        context.HttpContext.Response.Headers.RetryAfter = "60";
        await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.", token);
    }
}
