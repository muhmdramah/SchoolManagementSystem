using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SchoolManagementSystem.Infrastructure.Context;
using System.Linq.Expressions;

namespace SchoolManagementSystem.Infrastructure.InfrastructureBases
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
        public virtual async Task<ICollection<T>> GetAllAsync(params Expression<Func<T, object>>[]? includeProperties)
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

        public async Task<ICollection<T>> GetAllPagedAsync(int pageNumber, int pageSize, params Expression<Func<T, object>>[]? includeProperties)
        {
            IQueryable<T> query = _dbSet;

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            // Ensure we don't skip negative amounts
            int skipAmount = (pageNumber - 1) * pageSize;

            var result = await query
                .Skip(skipAmount)
                .ToListAsync();

            return result;
        }

        public virtual async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[]? includeProperties)
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

        public IQueryable<T> GetTableNoTracking()
        {
            return _dbSet.AsNoTracking().AsQueryable();

        }

        public IQueryable<T> GetTableAsTracking()
        {
            return _dbSet.AsTracking().AsQueryable();
        }
        #endregion

        #region Commands
        public virtual async Task<T> AddAsync(T entity)
        {
            var result = await _dbSet.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task AddRangeAsync(ICollection<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateRangeAsync(ICollection<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }

            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteRangeAsync(ICollection<T> entities)
        {
            foreach (var entity in entities)
            {
                _dbSet.Entry(entity).State = EntityState.Deleted;
            }
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Database Operations
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Commit()
        {
            _context.Database.CommitTransaction();
        }

        public void Rollback()
        {
            _context.Database.RollbackTransaction();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
        #endregion
    }
}
