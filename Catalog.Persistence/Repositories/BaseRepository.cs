using Catalog.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence.Repositories;

public class BaseRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
{
    protected readonly CatalogDbContext _dbContext;

    public BaseRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<TEntity> AddAsync(TEntity entity) 
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TEntity> GetByIdAsync<TId>(TId id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id)
            ?? throw new InvalidOperationException($"{typeof(TEntity).Name} with id '{id}' was not found.");
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }


    public async Task DeleteAsync(TEntity entity) 
    {
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<TEntity>> ListAllAsAsync()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }
}
