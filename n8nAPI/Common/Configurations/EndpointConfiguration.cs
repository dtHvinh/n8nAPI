namespace n8nAPI.Common.Configurations;

public class EndpointConfiguration
{
    public int Version { get; set; }

    public string GetPrefix()
    {
        return $"/api/v{Version}";
    }
}
