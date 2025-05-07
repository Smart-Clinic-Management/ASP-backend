using SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;

namespace SmartClinic.Application.Services.Implementation;
public class PagedCreator<T> : IPagedCreator<T>
    where T : BaseEntity
{

    public async Task<Pagination<T>> CreatePagedResult(IGenericRepo<T> repo,
               ISpecification<T> spec, int pageIndex, int pageSize)
    {
        var items = await repo.ListAsync(spec);
        var count = await repo.CountAsync(spec);

        var pagination = new Pagination<T>(pageIndex, pageSize, count, items);

        return pagination;
    }


    public async Task<Pagination<TDto>> CreatePagedResult<TDto>(IGenericRepo<T> repo,
      ISpecification<T, TDto> spec, int pageIndex, int pageSize)
    {
        var items = await repo.ListAsync(spec);

        var count = await repo.CountAsync(spec);

        List<TDto> dtoItems = [.. items];

        var pagination = new Pagination<TDto>(pageIndex, pageSize, count, dtoItems);

        return pagination;
    }
}
