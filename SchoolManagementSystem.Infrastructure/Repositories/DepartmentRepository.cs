using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Infrastructure.Context;
using SchoolManagementSystem.Infrastructure.InfrastructureBases;
using SchoolManagementSystem.Infrastructure.Interfaces;

namespace SchoolManagementSystem.Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
