namespace shopveeAPI.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<int> Add(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(Guid id);
    }
}