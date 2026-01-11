using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using n8nAPI.MinimalAPI.Extensions;
using n8nAPI.MinimalAPI.Interfaces;

namespace n8nAPI.MinimalAPI.Base;

public abstract partial class EndpointBase
{
    protected RouteGroupBuilder _group { get; set; } = default!;

    public abstract IEndpointRouteBuilder RegisterEndpoint(IEndpointRouteBuilder routeBuilder);

    public EndpointBase Group(RouteGroupBuilder group)
    {
        GuardedEndpointBase.IsAvailableToSetGroup(_group);
        _group = group;

        return this;
    }

    public EndpointBase Group(EndpointBase group)
    {
        GuardedEndpointBase.IsAvailableToSetGroup(_group);
        _group = group._group;

        return this;
    }

    public EndpointBase Group<TGroup>(IEndpointRouteBuilder routeBuilder) where TGroup : GroupBase
    {
        GuardedEndpointBase.IsAvailableToSetGroup(_group);
        TGroup group = Activator.CreateInstance<TGroup>();

        _group = group.CreateGroup(routeBuilder);

        return this;
    }

    public EndpointBase WithMetadata(Action<EndpointMetadataEnricher> metadata)
    {
        EndpointMetadataEnricher metadataEnricher = new();
        metadata(metadataEnricher);

        _group.EnrichEndpoint(metadataEnricher);
        return this;
    }

    public EndpointBase WithService(Action<EndpointServiceEnricher> service)
    {
        EndpointServiceEnricher serviceEnricher = new();
        service(serviceEnricher);

        _group.EnrichEndpoint(serviceEnricher);
        return this;
    }

    public EndpointBase EnrichEndpoint(params IEndpointEnricher[] enrichers)
    {
        _group.EnrichEndpoint(enrichers);
        return this;
    }

    /// <summary>
    /// Allow anonymous access to this endpoint
    /// </summary>
    /// <returns>Self</returns>
    public EndpointBase AllowAnonymous()
    {
        _group.AllowAnonymous();
        return this;
    }

    public EndpointBase MapGet(string pattern,
                               Delegate handler,
                               Action<EndpointMetadataEnricher> metadataConfig = default!,
                               Action<EndpointServiceEnricher> serviceConfig = default!)
    {

        Map(HttpMethod.Get, pattern, handler, metadataConfig, serviceConfig);
        return this;
    }

    private void Map(HttpMethod method,
                     string pattern,
                     Delegate handler,
                     Action<EndpointMetadataEnricher> metadataConfig = default!,
                     Action<EndpointServiceEnricher> serviceConfig = default!)
    {
        EndpointMetadataEnricher metadata = new();
        metadataConfig?.Invoke(metadata);

        EndpointServiceEnricher service = new();
        serviceConfig?.Invoke(service);

        IEndpointEnricher[] enrichers = [metadata, service];

        switch (method.Method)
        {
            case "GET":
                _group.MapGet(pattern, handler).EnrichEndpoint(enrichers);
                break;
            case "POST":
                _group.MapPost(pattern, handler).EnrichEndpoint(enrichers);
                break;
            case "PUT":
                _group.MapPut(pattern, handler).EnrichEndpoint(enrichers);
                break;
            case "DELETE":
                _group.MapDelete(pattern, handler).EnrichEndpoint(enrichers);
                break;
            case "PATCH":
                _group.MapPatch(pattern, handler).EnrichEndpoint(enrichers);
                break;
            default:
                throw new NotSupportedException($"HTTP method {method} is not supported.");
        }
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
