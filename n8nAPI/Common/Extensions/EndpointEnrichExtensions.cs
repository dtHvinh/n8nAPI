using n8nAPI.Common.Interfaces;

namespace n8nAPI.Common.Extensions;

public static class EndpointEnrichExtensions
{
    public static RouteHandlerBuilder EnrichEndpoint(this RouteHandlerBuilder builder, params IEndpointEnricher[] enrichers)
    {
        foreach (var enricher in enrichers)
        {
            builder = enricher.Enrich(builder);
        }

        return builder;
    }
}
