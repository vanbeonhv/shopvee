using Microsoft.EntityFrameworkCore;
using shopveeAPI.DbContext;

namespace shopveeAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ShopveeDbContext _shopveeDbContext;

        public GenericRepository(ShopveeDbContext shopveeDbContext)
        {
            _shopveeDbContext = shopveeDbContext;
        }

        public async Task<int> Add(T entity)
        {
            _shopveeDbContext.Add(entity);
            return await _shopveeDbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await _shopveeDbContext.Set<T>().AsQueryable().ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _shopveeDbContext.Set<T>().FindAsync(id)
                   ??
                   throw new ArgumentNullException($"Entity with id {id} not found.");
        }

        public async Task Update(T entity)
        {
            _shopveeDbContext.Update(entity);
            await _shopveeDbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(T entity)
        {
            _shopveeDbContext.Remove(entity);
            await _shopveeDbContext.SaveChangesAsync();
            return 1;
        }
    }
}