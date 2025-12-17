namespace n8nAPI.APIWrapper.Common.Pagination;

public class CursorResult<T>
{
    public IReadOnlyList<T> Data { get; init; } = default!;
    public string? NextCursor { get; init; }
}
