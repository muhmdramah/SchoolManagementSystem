using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace SchoolManagementSystem.Infrastructure.InfrastructureBases
{
    public interface IGenericRepository<T> where T : class
    {
        Task<ICollection<T>> GetAllAsync(params Expression<Func<T, object>>[]? includeProperties);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[]? includeProperties);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();

        Task AddRangeAsync(ICollection<T> entities);
        Task UpdateRangeAsync(ICollection<T> entities);
        Task DeleteRangeAsync(ICollection<T> entities);

        Task SaveChangesAsync();
        void Commit();
        void Rollback();
        IDbContextTransaction BeginTransaction();

    }
}
