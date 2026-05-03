using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Infrastructure.Context;
using SchoolManagementSystem.Infrastructure.Interfaces;
using System.Linq.Expressions;

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
        public async Task<ICollection<T>> GetAllAsync(params Expression<Func<T, object>>[]? includeProperties)
        {
            IQueryable<T> query = _dbSet;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            var result = await query.ToListAsync();

            return result;
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[]? includeProperties)
        {
            IQueryable<T> query = _dbSet;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            var result = await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "StudentId") == id);

            return result!;
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
            _context.SaveChanges();
            return result.Entity;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        #endregion
    }
}