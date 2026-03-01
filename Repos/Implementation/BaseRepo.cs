using Microsoft.EntityFrameworkCore;
using Apps_Apis.Data;
using Apps_Apis;

namespace Apps_Apis.Repos.Implementation;

public class BaseRepo<T> : IBaseRepository<T> where T : class
{
    private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepo(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // IQueryable (flexible queries)
        public IQueryable<T> GetAllAsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }
}
