namespace SmartClinic.Application.Bases;
public class Pagination<T>(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
{
    public int PageIndex { get; set; } = pageIndex;
    public int PageSize { get; set; } = pageSize;
    public int TotalPages { get; set; } = (int)Math.Floor((decimal)(count / pageSize));
    public int Total { get; set; } = count;
    public IReadOnlyList<T> Data { get; set; } = data;
}
