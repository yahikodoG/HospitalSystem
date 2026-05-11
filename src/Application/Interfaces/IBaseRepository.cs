namespace Application.Interfaces;

public interface IBaseRepository<T> where T : class
{
    IQueryable<T> GetQueryable();
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}