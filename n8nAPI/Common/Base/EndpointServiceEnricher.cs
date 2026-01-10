using n8nAPI.Common.Interfaces;

namespace n8nAPI.Common.Base;

public class EndpointServiceEnricher : IEndpointEnricher
{
    public string RateLimiterPolicyName { get; set; } = string.Empty;

    public RouteHandlerBuilder Enrich(RouteHandlerBuilder builder)
    {
        if (!string.IsNullOrEmpty(RateLimiterPolicyName))
        {
            builder.RequireRateLimiting(RateLimiterPolicyName);
        }

        return builder;
    }
}
