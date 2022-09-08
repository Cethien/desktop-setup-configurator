namespace DSC.Repositories;

public interface IRepository<T>
{
    Task<bool> AddAsync(T entity);
    Task<bool> UpdateAsync(object id, T updateData);
    Task<T> GetByIdAsync(object id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<bool> RemoveAsync(object id);
}
