using n8nAPI.APIWrapper.Common.Interfaces;

namespace n8nAPI.APIWrapper.Models;

public class Tag : IDateTimeInfo
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
