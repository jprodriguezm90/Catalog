namespace Catalog.Application.Contracts.Persistence;

public interface IAsyncRepository<TEntity> where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> GetByIdAsync<TId>(TId id);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}
