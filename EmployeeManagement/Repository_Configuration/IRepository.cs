using System.Linq.Expressions;

namespace EmployeeManagement.Repository_Configuration
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetByIdAsync<TId>(TId id);
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> FindFirstAsync<TProperty>(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, TProperty>>[] includes);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> DeleteAsync<TId>(TId id);
        Task<bool> DeleteAsync(TEntity entity);
        Task BulkInsertAsync(IEnumerable<TEntity> entities);
        Task BulkUpdateAsync(IEnumerable<TEntity> entities);
        Task BulkUpdateAsync(Expression<Func<TEntity, bool>> predicate, Action<TEntity> updateAction);
    }
}
