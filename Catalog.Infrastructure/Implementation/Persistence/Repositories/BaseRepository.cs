using Catalog.Application.Contracts.Persistence;

namespace Catalog.Infrastructure.Implementation.Persistence.Repositories;

public class BaseRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
{
    private readonly CatalogDbContext _context;

    public BaseRepository(CatalogDbContext context)
    {
        _context = context;
    }

    public async Task<TEntity> AddAsync(TEntity entity) 
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TEntity> GetByIdAsync<TId>(TId id)
    {
        return await _context.Set<TEntity>().FindAsync(id)
            ?? throw new InvalidOperationException($"{typeof(TEntity).Name} with id '{id}' was not found.");
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(TEntity entity) 
    {
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    
}
