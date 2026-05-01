using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Infrastructure.Context;
using SchoolManagementSystem.Infrastructure.Interfaces;

namespace SchoolManagementSystem.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Fileds
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        #endregion

        #region Constructors
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        #endregion

        #region Queries
        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        #endregion

        #region Commands
        public async Task<T> AddAsync(T entity)
        {
            var result = await _dbSet.AddAsync(entity);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public T Update(T entity)
        {
            var result = _dbSet.Update(entity);
            return result.Entity;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        #endregion
    }
}