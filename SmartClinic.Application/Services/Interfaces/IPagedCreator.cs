namespace SmartClinic.Application.Services.Interfaces;
public interface IPagedCreator<T>
    where T : BaseEntity
{
    Task<Pagination<T>> CreatePagedResult(IGenericRepo<T> repo, ISpecification<T> spec, int pageIndex, int pageSize);

    Task<Pagination<TDto>> CreatePagedResult<TDto>(IGenericRepo<T> repo,
      ISpecification<T, TDto> spec, int pageIndex, int pageSize);

}
