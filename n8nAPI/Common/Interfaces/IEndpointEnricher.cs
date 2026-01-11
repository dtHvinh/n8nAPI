namespace n8nAPI.Common.Interfaces;

public interface IEndpointEnricher
{
    IEndpointConventionBuilder Enrich(IEndpointConventionBuilder builder);
}
