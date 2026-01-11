using Microsoft.AspNetCore.Builder;
using n8nAPI.MinimalAPI.Interfaces;

namespace n8nAPI.MinimalAPI.Extensions;

public static class EndpointEnrichExtensions
{
    public static IEndpointConventionBuilder EnrichEndpoint(this IEndpointConventionBuilder builder, params IEndpointEnricher[] enrichers)
    {
        foreach (var enricher in enrichers)
        {
            builder = enricher.Enrich(builder);
        }

        return builder;
    }
}
