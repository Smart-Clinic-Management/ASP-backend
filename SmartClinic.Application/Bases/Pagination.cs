namespace SmartClinic.Application.Bases;
public class Pagination<T>(int pageIndex, int pageSize, int count, IEnumerable<T> data)
{
    public int PageIndex { get; set; } = pageIndex;
    public int PageSize { get; set; } = pageSize;
    public int TotalPages { get; set; } = (int)Math.Ceiling((decimal)count / pageSize);
    public int Total { get; set; } = count;
    public IEnumerable<T> Data { get; set; } = data;
}
