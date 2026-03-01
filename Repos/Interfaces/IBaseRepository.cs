namespace Apps_Apis;

public interface IBaseRepository<T> where T : class
{
    // Flexible queries
        IQueryable<T> GetAllAsQueryable();

        // Direct execution
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(object id);

        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);

        void Update(T entity);
        void Delete(T entity);
}
