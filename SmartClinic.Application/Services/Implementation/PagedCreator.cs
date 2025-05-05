namespace SmartClinic.Application.Services.Implementation;
public class PagedCreator<T> : IPagedCreator<T>
    where T : BaseEntity
{


    Pagination<T> IPagedCreator<T>.CreatePagedResult(IReadOnlyList<T> data, int pageIndex, int pageSize, int count
       )
    {
        return new Pagination<T>(pageIndex, pageSize, count, data);
    }

    Pagination<TDto> IPagedCreator<T>.CreatePagedResult<TDto>(IReadOnlyList<TDto> data, int pageIndex, int pageSize,
       int count)
       => new(pageIndex, pageSize, count, data);
}
