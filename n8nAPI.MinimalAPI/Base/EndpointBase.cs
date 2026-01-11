using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using n8nAPI.MinimalAPI.Extensions;
using n8nAPI.MinimalAPI.Interfaces;

namespace n8nAPI.MinimalAPI.Base;

public abstract partial class EndpointBase : EndpointBuilderBase
{
    public abstract IEndpointRouteBuilder RegisterEndpoint(IEndpointRouteBuilder routeBuilder);

    public EndpointBase MapGet(string pattern,
                               Delegate handler,
                               Action<EndpointMetadataEnricher> metadataConfig = default!,
                               Action<EndpointServiceEnricher> serviceConfig = default!)
    {

        Map(HttpMethod.Get, pattern, handler, metadataConfig, serviceConfig);
        return this;
    }

    public EndpointBase MapPost(string pattern,
                                Delegate handler,
                                Action<EndpointMetadataEnricher> metadataConfig = default!,
                                Action<EndpointServiceEnricher> serviceConfig = default!)
    {
        Map(HttpMethod.Post, pattern, handler, metadataConfig, serviceConfig);
        return this;
    }

    public EndpointBase MapPut(string pattern,
                               Delegate handler,
                               Action<EndpointMetadataEnricher> metadataConfig = default!,
                               Action<EndpointServiceEnricher> serviceConfig = default!)
    {
        Map(HttpMethod.Put, pattern, handler, metadataConfig, serviceConfig);
        return this;
    }

    public EndpointBase MapDelete(string pattern,
                                  Delegate handler,
                                  Action<EndpointMetadataEnricher> metadataConfig = default!,
                                  Action<EndpointServiceEnricher> serviceConfig = default!)
    {
        Map(HttpMethod.Delete, pattern, handler, metadataConfig, serviceConfig);
        return this;
    }

    public EndpointBase MapPatch(string pattern,
                                 Delegate handler,
                                 Action<EndpointMetadataEnricher> metadataConfig = default!,
                                 Action<EndpointServiceEnricher> serviceConfig = default!)
    {
        Map(HttpMethod.Patch, pattern, handler, metadataConfig, serviceConfig);
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

}
