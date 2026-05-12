using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Infrastructure.Context;

namespace SchoolManagementSystem.Infrastructure.InfrastructureBases
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGenericRepository<Student> StudentRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context, IGenericRepository<Student> studentRepository)
        {
            _context = context;
            StudentRepository = new GenericRepository<Student>(context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
