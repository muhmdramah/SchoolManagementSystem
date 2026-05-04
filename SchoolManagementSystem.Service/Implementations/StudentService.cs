using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Infrastructure.InfrastructureBases;
using SchoolManagementSystem.Service.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagementSystem.Service.Implementations
{
    public class StudentService : IStudentService
    {
        #region Fields
        private readonly IGenericRepository<Student> _genericRepository;
        #endregion

        #region Constructors
        public StudentService(IGenericRepository<Student> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        #endregion

        #region Queries
        public async Task<ICollection<Student>> GetStudentsAsync()
        {
            return await _genericRepository
                .GetAllAsync(new Expression<Func<Student, object>>[] { s => s.Department });
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _genericRepository
                .GetByIdAsync(id, new Expression<Func<Student, object>>[] { s => s.Department });
        }

        public async Task DeleteStudentAsync(Student student)
        {
            await _genericRepository.DeleteAsync(student);
        }
        #endregion
    }
}
