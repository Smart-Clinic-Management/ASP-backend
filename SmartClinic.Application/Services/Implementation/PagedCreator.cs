using SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;

namespace SmartClinic.Application.Services.Implementation;
public class PagedCreator<T> : IPagedCreator<T>
    where T : BaseEntity
{


    async Task<Pagination<T>> IPagedCreator<T>.CreatePagedResult(IRepository<T> repo, int pageIndex, int pageSize,
        IReadOnlyList<T> data)
    {
        var count = await repo.CountAsync();
        return new Pagination<T>(pageIndex, pageSize, count, data);
    }

    async Task<Pagination<TDto>> IPagedCreator<T>.CreatePagedResult<TDto>(IRepository<T> repo, int pageIndex, int pageSize,
        IReadOnlyList<TDto> data)
    {
        var count = await repo.CountAsync();
        return new Pagination<TDto>(pageIndex, pageSize, count, data);
    }
}
