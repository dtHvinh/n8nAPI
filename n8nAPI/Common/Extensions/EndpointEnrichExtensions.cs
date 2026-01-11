using n8nAPI.Common.Interfaces;

namespace n8nAPI.Common.Extensions;

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
