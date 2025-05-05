namespace SmartClinic.Application.Bases;
public class PagingParams
{
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string? OrderBy { get; set; }
    public bool IsDescending { get; set; }
}
