namespace n8nAPI.Common.Base;

public abstract class GroupBase : EndpointBase
{
    public abstract RouteGroupBuilder CreateGroup(IEndpointRouteBuilder routeBuilder);
}
