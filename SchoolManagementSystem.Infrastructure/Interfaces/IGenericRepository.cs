using System.Linq.Expressions;

namespace SchoolManagementSystem.Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<ICollection<T>> GetAllAsync(params Expression<Func<T, object>>[]? includeProperties);
        public Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[]? includeProperties);
        public Task<T> AddAsync(T entity);
        public T Update(T entity);
        public Task DeleteByIdAsync(int id);
    }
}
