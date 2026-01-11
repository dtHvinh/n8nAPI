using n8nAPI.APIWrapper.Common.Interfaces;

namespace n8nAPI.APIWrapper.Models;

public class SharedWorkflow : IDateTimeInfo
{
    public string Role { get; set; }
    public string WorkflowId { get; set; }
    public string ProjectId { get; set; }
    public SharedProject Project { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class SharedProject
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
}
