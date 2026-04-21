
using Market.Repositories.Data;
using Market.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Market.Repositories.Implementations;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly MarketDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(MarketDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    public async Task AddAsync(T entity)
    {
        _dbSet.Add(entity);
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T> GetByIdAsync(object id) => await _dbSet.FindAsync(id);

    public void Remove(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
}
