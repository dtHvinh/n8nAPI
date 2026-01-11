using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using n8nAPI.APIWrapper.Core.Client;
using n8nAPI.APIWrapper.Core.Pagniation;
using n8nAPI.APIWrapper.Models;
using n8nAPI.MinimalAPI.Base;

namespace n8nAPI.Endpoints.Workflows.RetrieveAll;

public class Endpoint : EndpointBase
{
    public override IEndpointRouteBuilder RegisterEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        Group<WorkflowGroup>(routeBuilder);

        MapGet("", HandleAsync);

        return routeBuilder;
    }

    public static async Task<Results<Ok<PageResponse<Workflow>>, ProblemHttpResult>> HandleAsync(
        [FromServices] WorkflowClient client,
        CancellationToken cancellationToken)
    {
        var res = await client.GetWorkflowsAsync(cancellationToken);
        return TypedResults.Ok(res);
    }
}
