using n8nAPI.APIWrapper.Common.Interfaces;

namespace n8nAPI.APIWrapper.Models;

public class Workflow : IDateTimeInfo
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public List<Node> Nodes { get; set; }
    public Dictionary<string, Connection> Connections { get; set; }
    public WorkflowSettings Settings { get; set; }
    public Dictionary<string, object> StaticData { get; set; }
    public List<Tag> Tags { get; set; }
    public List<SharedWorkflow> SharedWorkflow { get; set; }
    public ActiveVersion ActiveVersion { get; set; }
}
