using n8nAPI.Common.Base;

namespace n8nAPI.Endpoints.Workflows.RetrieveAll;

public class Endpoint : EndpointBase
{
    public override IEndpointRouteBuilder RegisterEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        Group<WorkflowGroup>(routeBuilder);

        MapGet("test", Test);

        return routeBuilder;
    }

    public static IResult Test()
    {

    }
}
