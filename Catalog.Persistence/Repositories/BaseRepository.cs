using Catalog.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence.Repositories;

public class BaseRepository<TEntity>(CatalogDbContext context) : IAsyncRepository<TEntity> where TEntity : class
{
    public async Task<TEntity> AddAsync(TEntity entity) 
    {
        await context.Set<TEntity>().AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TEntity> GetByIdAsync<TId>(TId id)
    {
        return await context.Set<TEntity>().FindAsync(id)
            ?? throw new InvalidOperationException($"{typeof(TEntity).Name} with id '{id}' was not found.");
    }

    public async Task UpdateAsync(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
        await context.SaveChangesAsync();
    }


    public async Task DeleteAsync(TEntity entity) 
    {
        context.Set<TEntity>().Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<TEntity>> ListAllAsAsync()
    {
        return await context.Set<TEntity>().ToListAsync();
    }
}
