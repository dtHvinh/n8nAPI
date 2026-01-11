using n8nAPI.Common.Enums;
using n8nAPI.MinimalAPI.Base;

namespace n8nAPI.Endpoints.System.HealthCheck;

public class Endpoint : EndpointBase
{
    private static readonly DateTime _siteStartTime = DateTime.UtcNow;

    public override IEndpointRouteBuilder RegisterEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        Group(routeBuilder.MapGroup(Groups.System));
        AllowAnonymous();

        MapGet("/health", HandleGetHealth,
            mf =>
        {
            mf.Name = "Health check";
            mf.Description = "Get the app health check";
            mf.ProducesType = typeof(HealthCheckResponse);
        },
            sf =>
        {

        });

        return routeBuilder;
    }

    public static IResult HandleGetHealth()
    {
        var response = new HealthCheckResponse(
            UptimeInSeconds: (int)(DateTime.UtcNow - _siteStartTime).TotalSeconds,
            CurrentServerTime: DateTime.UtcNow,
            Message: "n8n is healthy");

        return TypedResults.Ok(response);
    }
}
