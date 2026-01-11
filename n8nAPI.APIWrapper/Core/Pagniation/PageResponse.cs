namespace n8nAPI.APIWrapper.Core.Pagniation;

public class PageResponse<T> where T : class
{
    public List<T> Data { get; set; } = default!;
    public string NextCursor { get; set; }
}
