using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Infrastructure.Interfaces;
using SchoolManagementSystem.Service.Interfaces;

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
            return await _studentRepository.GetAllAsync();
        }
        #endregion
    }
}
