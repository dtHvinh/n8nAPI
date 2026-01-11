using n8nAPI.APIWrapper.Common.Interfaces;

namespace n8nAPI.APIWrapper.Models;

public class ActiveVersion : IDateTimeInfo
{
    public string VersionId { get; set; }
    public string WorkflowId { get; set; }
    public List<Node> Nodes { get; set; }
    public Dictionary<string, Connection> Connections { get; set; }
    public string Authors { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
