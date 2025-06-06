﻿namespace SmartClinic.Infrastructure.Repos;

public class GenericRepo<T>(ApplicationDbContext _context) : IGenericRepo<T>
       where T : BaseEntity
{
    private readonly DbSet<T> _db = _context.Set<T>();

    public async Task AddAsync(T entity) => await _db.AddAsync(entity);

    public async Task<int> CountAsync(ISpecification<T> spec) => await _db.CountAsync(spec.Criteria!);

    public void Delete(T entity) => _db.Remove(entity);

    public Task<bool> ExistsAsync(int id) => _db.AnyAsync(x => x.Id == id);

    public Task<bool> ExistsWithSpecAsync(ISpecification<T> spec) => _db.AnyAsync(spec.Criteria!);

    public async Task<T?> GetByIdAsync(int id) => await _db.FindAsync(id);

    public async Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec)
        => await ApplySecifications(spec).FirstOrDefaultAsync();

    public async Task<TResult?> GetEntityWithSpecAsync<TResult>(ISpecification<T, TResult> spec)
        => await ApplySecifications(spec).FirstOrDefaultAsync();

    public async Task<IEnumerable<T>> ListAsync(ISpecification<T> spec)
        => await ApplySecifications(spec).ToListAsync();

    public async Task<IEnumerable<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec)
        => await ApplySecifications(spec).ToListAsync();

    public void Update(T entity) => _db.Update(entity);

    private IQueryable<T> ApplySecifications(ISpecification<T> spec)
        => SpecificationEvaluator<T>.GetQuery(_db.AsQueryable(), spec);

    private IQueryable<TResult> ApplySecifications<TResult>(ISpecification<T, TResult> spec)
       => SpecificationEvaluator<T>.GetQuery(_db.AsQueryable(), spec);



}
