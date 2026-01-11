using Microsoft.AspNetCore.Routing;

namespace n8nAPI.MinimalAPI.Base;

public abstract class GroupBase : EndpointBuilderBase
{
    public abstract RouteGroupBuilder CreateGroup(IEndpointRouteBuilder routeBuilder);
}
