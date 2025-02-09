using System.Linq.Expressions;
using EmployeeManagement.DB_Configuration;
using Microsoft.EntityFrameworkCore;
namespace EmployeeManagement.Repository_Configuration
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDBContext _context;
        private DbSet<TEntity> _dbSet;
        public Repository(ApplicationDBContext context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>();
        }
        public async Task BulkInsertAsync(IEnumerable<TEntity> entities)
        {
            await _context.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public Task BulkUpdateAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public async Task BulkUpdateAsync(Expression<Func<TEntity, bool>> predicate, Action<TEntity> updateAction)
        {
            var entities = await _dbSet.Where(predicate).ToListAsync();
            if (entities == null || !entities.Any())
            {
                return;
            }

            foreach (var entity in entities)
            {
                updateAction(entity);
            }

            _context.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public Task<bool> DeleteAsync<TId>(TId id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> FindAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindFirstAsync<TProperty>(Expression<Func<TEntity, bool>> predicate, params System.Linq.Expressions.Expression<Func<TEntity, TProperty>>[] includes)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync(params System.Linq.Expressions.Expression<Func<TEntity, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync<TId>(TId id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, params System.Linq.Expressions.Expression<Func<TEntity, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> InsertAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
