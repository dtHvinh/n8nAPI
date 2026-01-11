using Microsoft.AspNetCore.Builder;

namespace n8nAPI.MinimalAPI.Interfaces;

public interface IEndpointEnricher
{
    IEndpointConventionBuilder Enrich(IEndpointConventionBuilder builder);
}
