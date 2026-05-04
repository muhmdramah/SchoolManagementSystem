using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Infrastructure.InfrastructureBases;
using SchoolManagementSystem.Service.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagementSystem.Service.Implementations
{
    public class StudentService : IStudentService
    {
        #region Fields
        private readonly IGenericRepository<Student> _studentRepository;
        #endregion

        #region Constructors
        public StudentService(IGenericRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }
        #endregion

        #region Queries
        public async Task<ICollection<Student>> GetStudentsAsync()
        {
            return await _studentRepository.GetAllAsync(new Expression<Func<Student, object>>[] { s => s.Department });
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _studentRepository.GetByIdAsync(id, new Expression<Func<Student, object>>[] { s => s.Department });
        }

        public async Task DeleteStudentAsync(Student student)
        {
            await _studentRepository.DeleteAsync(student);
        }
        #endregion
    }
}
