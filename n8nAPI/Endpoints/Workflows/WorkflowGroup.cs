using n8nAPI.Common.Constants;
using n8nAPI.Common.Enums;
using n8nAPI.MinimalAPI.Base;

namespace n8nAPI.Endpoints.Workflows;

public class WorkflowGroup : GroupBase
{
    public override RouteGroupBuilder CreateGroup(IEndpointRouteBuilder routeBuilder)
    {
        Group(routeBuilder.MapGroup(Groups.Workflow));

        WithMetadata(cf =>
        {
            cf.Name = "Workflows";
            cf.Description = "Endpoints for managing workflows.";
            cf.Tags = [EndpointTags.Workflows, EndpointTags.Core];
        });

        return _group;
    }
}
