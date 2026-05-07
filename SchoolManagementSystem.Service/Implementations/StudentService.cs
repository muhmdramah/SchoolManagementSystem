using Microsoft.EntityFrameworkCore;
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
            //return await _genericRepository
            //    .GetByIdAsync(id, new Expression<Func<Student, object>>[] { s => s.Department });

            var student = await _genericRepository.GetTableNoTracking()
                                           .Include(s => s.Department)
                                           .FirstOrDefaultAsync(s => s.StudentId == id);
            return student;
        }
        #endregion

        #region Commands
        public async Task<string> AddStudentAsync(Student student)
        {
            // dont use threading with queryable.. it will cause issues with the db context.
            var studentExists = _genericRepository
                .GetTableNoTracking()
                .FirstOrDefaultAsync(s => s.StudentName.Equals(student.StudentName));

            if (studentExists is not null)
                return "Student you are trying to add is already exist... try add anothe one!";

            await _genericRepository.AddAsync(student);

            return "Student created successfully!";
        }

        public async Task DeleteStudentAsync(Student student)
        {
            await _genericRepository.DeleteAsync(student);
        }
        #endregion
    }
}
