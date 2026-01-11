using Microsoft.AspNetCore.Builder;
using n8nAPI.MinimalAPI.Interfaces;

namespace n8nAPI.MinimalAPI.Base;

public class EndpointServiceEnricher : IEndpointEnricher
{
    public string RateLimiterPolicyName { get; set; } = string.Empty;

    public IEndpointConventionBuilder Enrich(IEndpointConventionBuilder builder)
    {
        if (!string.IsNullOrEmpty(RateLimiterPolicyName))
        {
            builder.RequireRateLimiting(RateLimiterPolicyName);
        }

        return builder;
    }
}
