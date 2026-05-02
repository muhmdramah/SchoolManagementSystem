namespace SchoolManagementSystem.Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<ICollection<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task<T> AddAsync(T entity);
        public T Update(T entity);
        public Task DeleteByIdAsync(int id);
    }
}
