using Microsoft.AspNetCore.Routing;

namespace n8nAPI.MinimalAPI.Base;

public abstract class GroupBase : EndpointBase
{
    public abstract RouteGroupBuilder CreateGroup(IEndpointRouteBuilder routeBuilder);
}
