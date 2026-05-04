using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace SchoolManagementSystem.Infrastructure.InfrastructureBases
{
    public interface IGenericRepository<T> where T : class
    {
        #region Queries 
        Task<ICollection<T>> GetAllAsync(params Expression<Func<T, object>>[]? includeProperties);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[]? includeProperties);
        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();
        #endregion

        #region Commands
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task AddRangeAsync(ICollection<T> entities);
        Task UpdateRangeAsync(ICollection<T> entities);
        Task DeleteRangeAsync(ICollection<T> entities);
        #endregion

        #region Database Operations
        Task SaveChangesAsync();
        void Commit();
        void Rollback();
        IDbContextTransaction BeginTransaction();
        #endregion
    }
}
