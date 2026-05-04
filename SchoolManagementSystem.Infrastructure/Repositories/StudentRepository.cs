using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Infrastructure.Context;
using SchoolManagementSystem.Infrastructure.InfrastructureBases;
using SchoolManagementSystem.Infrastructure.Interfaces;

namespace SchoolManagementSystem.Infrastructure.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
