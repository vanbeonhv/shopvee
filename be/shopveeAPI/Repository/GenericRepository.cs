using System.Data;
using Microsoft.EntityFrameworkCore;
using shopveeAPI.Common;
using shopveeAPI.DbContext;

namespace shopveeAPI.Repository
{
    public class GenericRepository<T> : Service, IGenericRepository<T> where T : class
    {
        private readonly ShopveeDbContext _shopveeDbContext;

        public GenericRepository(ShopveeDbContext shopveeDbContext)
        {
            _shopveeDbContext = shopveeDbContext;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _shopveeDbContext.Set<T>().AsQueryable().ToListAsync();
        }
        
        public async Task<int> Add(T entity)
        {
            _shopveeDbContext.Add(entity);
            return await _shopveeDbContext.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _shopveeDbContext.Set<T>().FindAsync(id)
                   ??
                   throw new ArgumentNullException($"Entity with id {id} not found.");
        }

        public async Task<int> Update(T entity)
        {
            _shopveeDbContext.Update(entity);
            await _shopveeDbContext.SaveChangesAsync();
            return 1;
        }

        public async Task<int> Delete(Guid id)
        {
            var entity = await _shopveeDbContext.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return 0;
            }

            _shopveeDbContext.Set<T>().Remove(entity);
            await _shopveeDbContext.SaveChangesAsync();
            return 1;
        }
    }
}