using SmartClinic.Application.Services.Interfaces.InfrastructureInterfaces;

namespace SmartClinic.Application.Services.Interfaces;
public interface IPagedCreator<T>
    where T : BaseEntity
{
    Task<Pagination<T>> CreatePagedResult(IRepository<T> repo, int pageIndex, int pageSize, IReadOnlyList<T> data);

    Task<Pagination<TDto>> CreatePagedResult<TDto>(IRepository<T> repo, int pageIndex, int pageSize, IReadOnlyList<TDto> data);

}
