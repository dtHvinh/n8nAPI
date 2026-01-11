using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using n8nAPI.MinimalAPI.Extensions;
using n8nAPI.MinimalAPI.Interfaces;

namespace n8nAPI.MinimalAPI.Base;

public abstract class EndpointBuilderBase
{
    protected RouteGroupBuilder _group = default!;

    public EndpointBuilderBase Group(RouteGroupBuilder group)
    {
        GuardedEndpointBase.IsAvailableToSetGroup(_group);
        _group = group;

        return this;
    }

    public EndpointBuilderBase Group(EndpointBase group)
    {
        GuardedEndpointBase.IsAvailableToSetGroup(_group);
        _group = group._group;

        return this;
    }

    public EndpointBuilderBase Group<TGroup>(IEndpointRouteBuilder routeBuilder) where TGroup : GroupBase
    {
        GuardedEndpointBase.IsAvailableToSetGroup(_group);
        TGroup group = Activator.CreateInstance<TGroup>();

        _group = group.CreateGroup(routeBuilder);

        return this;
    }

    public EndpointBuilderBase WithMetadata(Action<EndpointMetadataEnricher> metadata)
    {
        EndpointMetadataEnricher metadataEnricher = new();
        metadata(metadataEnricher);

        _group.EnrichEndpoint(metadataEnricher);
        return this;
    }

    public EndpointBuilderBase WithService(Action<EndpointServiceEnricher> service)
    {
        EndpointServiceEnricher serviceEnricher = new();
        service(serviceEnricher);

        _group.EnrichEndpoint(serviceEnricher);
        return this;
    }

    public EndpointBuilderBase EnrichEndpoint(params IEndpointEnricher[] enrichers)
    {
        _group.EnrichEndpoint(enrichers);
        return this;
    }

    /// <summary>
    /// Allow anonymous access to this endpoint
    /// </summary>
    /// <returns>Self</returns>
    public EndpointBuilderBase AllowAnonymous()
    {
        _group.AllowAnonymous();
        return this;
    }


    private static class GuardedEndpointBase
    {
        public static void IsAvailableToSetGroup(RouteGroupBuilder group)
        {
            if (group == null)
                return;

            throw new InvalidOperationException("Group has already been set and cannot be modified.");
        }
    }
}
