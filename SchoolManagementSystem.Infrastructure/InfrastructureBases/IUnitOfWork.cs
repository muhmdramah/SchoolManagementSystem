using SchoolManagementSystem.Data.Entities;

namespace SchoolManagementSystem.Infrastructure.InfrastructureBases
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Student> StudentRepository { get; }

        Task<int> CompleteAsync();
    }
}
