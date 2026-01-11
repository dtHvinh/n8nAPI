using n8nAPI.Common.Constants;
using n8nAPI.Common.Enums;
using n8nAPI.MinimalAPI.Base;
using ET = n8nAPI.Common.Constants.EndpointTags;

namespace n8nAPI.Endpoints.System.Test;

public class Endpoint : EndpointBase
{
    public override IEndpointRouteBuilder RegisterEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        Group(routeBuilder.MapGroup(Groups.Test));

        MapGet("/rate-limiter", TestRateLimiter,
            cf =>
            {
                cf.Name = "Test Rate Limiter";
                cf.Description = "An endpoint to test the rate limiter functionality.";
                cf.Tags = [ET.System, ET.Test, "Rate-Limiter"];
            },
            sf =>
            {
                sf.RateLimiterPolicyName = RateLimiters.TestLimiter;
            });

        return routeBuilder;
    }

    public static IResult TestRateLimiter()
    {
        return TypedResults.Ok(new
        {
            Message = "Rate limiter test successful."
        });
    }
}
