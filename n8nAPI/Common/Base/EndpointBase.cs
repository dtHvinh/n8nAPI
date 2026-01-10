using n8nAPI.Common.Extensions;
using n8nAPI.Common.Interfaces;

namespace n8nAPI.Common.Base;

public abstract partial class EndpointBase
{
    protected RouteGroupBuilder Group { get; set; } = default!;

    public abstract IEndpointRouteBuilder RegisterEndpoint(IEndpointRouteBuilder endpointRouteBuilder);

    public EndpointBase WithGroup(RouteGroupBuilder group)
    {
        Group = group;
        return this;
    }

    /// <summary>
    /// Allow anonymous access to this endpoint
    /// </summary>
    /// <returns>Self</returns>
    public EndpointBase AllowAnonymous()
    {
        Group.AllowAnonymous();
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
        metadataConfig(metadata);

        EndpointServiceEnricher service = new();
        serviceConfig(service);

        IEndpointEnricher[] enrichers = [metadata, service];

        switch (method.Method)
        {
            case "GET":
                Group.MapGet(pattern, handler).EnrichEndpoint(enrichers);
                break;
            case "POST":
                Group.MapPost(pattern, handler).EnrichEndpoint(enrichers);
                break;
            case "PUT":
                Group.MapPut(pattern, handler).EnrichEndpoint(enrichers);
                break;
            case "DELETE":
                Group.MapDelete(pattern, handler).EnrichEndpoint(enrichers);
                break;
            case "PATCH":
                Group.MapPatch(pattern, handler).EnrichEndpoint(enrichers);
                break;
            default:
                throw new NotSupportedException($"HTTP method {method} is not supported.");
        }
    }
}
