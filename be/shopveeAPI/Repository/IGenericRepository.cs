namespace shopveeAPI.Repository
{
  public interface IGenericRepository<T> where T : class
  {
    Task<T> GetById(Guid id);
    Task<List<T>> GetAll();
    Task<int> Add(T entity);
    Task Update(T entity);
    Task<int> Delete(Guid id);
  }
}