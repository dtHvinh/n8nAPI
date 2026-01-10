namespace n8nAPI.Common.Interfaces;

public interface IEndpointEnricher
{
    RouteHandlerBuilder Enrich(RouteHandlerBuilder builder);
}
