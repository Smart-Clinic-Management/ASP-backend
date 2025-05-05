namespace SmartClinic.Application.Services.Interfaces;
public interface IPagedCreator<T>
    where T : BaseEntity
{
    Pagination<T> CreatePagedResult(IReadOnlyList<T> data, int pageIndex, int pageSize, int count);

    Pagination<TDto> CreatePagedResult<TDto>(IReadOnlyList<TDto> data, int pageIndex, int pageSize, int count);

}
