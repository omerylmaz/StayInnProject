namespace BuildingBlocks.Pagination;

public class PaginatedResult<T>(int pageIndex, int pageSize, int count, IEnumerable<T> items) where T : class
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
    public int Count { get; } = count;
    public IEnumerable<T> Items { get; } = items;
}
