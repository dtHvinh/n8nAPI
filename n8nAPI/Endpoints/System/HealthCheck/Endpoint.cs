using n8nAPI.Common.Base;
using n8nAPI.Common.Enums;

namespace n8nAPI.Endpoints.System.HealthCheck;

public class Endpoint : EndpointBase
{
    private static readonly DateTime _siteStartTime = DateTime.UtcNow;

    public override IEndpointRouteBuilder RegisterEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        Group(endpointRouteBuilder.MapGroup(Groups.System));
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

        return endpointRouteBuilder;
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
