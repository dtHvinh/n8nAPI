using n8nAPI.APIWrapper.Common.Interfaces;

namespace n8nAPI.APIWrapper.Models;

public class Node : IDateTimeInfo
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string WebhookId { get; set; }
    public bool Disabled { get; set; }
    public bool NotesInFlow { get; set; }
    public string Notes { get; set; }
    public string Type { get; set; }
    public double TypeVersion { get; set; }
    public bool ExecuteOnce { get; set; }
    public bool AlwayOutputData { get; set; }
    public bool RetryOnFail { get; set; }
    public int MaxTries { get; set; }
    public int WaitBetweenTries { get; set; }
    public string OnError { get; set; }
    public List<double> Position { get; set; }
    public object Parameters { get; set; }
    public object Credentials { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
