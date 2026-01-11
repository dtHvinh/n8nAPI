namespace n8nAPI.APIWrapper.Models;

public class Connection
{
    public List<List<ConnectionItem>> Main { get; set; }
}

public class ConnectionItem
{
    public string Node { get; set; }
    public string Type { get; set; }
    public int Index { get; set; }
}