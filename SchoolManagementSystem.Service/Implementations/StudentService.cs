using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
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
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<StudentService> _logger;
        private const string StudentsCacheKey = "students";
        #endregion

        #region Constructors
        public StudentService(IGenericRepository<Student> genericRepository,
            IMemoryCache memoryCache,
            ILogger<StudentService> logger)
        {
            _genericRepository = genericRepository;
            _memoryCache = memoryCache;
            _logger = logger;
        }
        #endregion

        #region Queries
        public async Task<ICollection<Student>> GetStudentsAsync()
        {
            if (_memoryCache.TryGetValue(StudentsCacheKey, out ICollection<Student> cachedStudents))
            {
                _logger.LogInformation("Students retrieved from cache.");
                return cachedStudents;
            }

            _logger.LogInformation("Students retrieved from database.");

            var students = await _genericRepository
                .GetAllAsync(new Expression<Func<Student, object>>[] { s => s.Department });

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(30))
                .SetSize(1);

            _memoryCache.Set(StudentsCacheKey, students, cacheOptions);

            return students;
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
            _memoryCache.Remove(StudentsCacheKey);

            await _genericRepository.AddAsync(student);

            return "created";
        }

        public async Task<string> UpdateStudentAsync(Student student)
        {
            _memoryCache.Remove(StudentsCacheKey);

            await _genericRepository.UpdateAsync(student);

            return "updated";
        }

        public async Task DeleteStudentAsync(Student student)
        {
            _memoryCache.Remove(StudentsCacheKey);

            await _genericRepository.DeleteAsync(student);
        }
        #endregion

        #region Helpers 
        public async Task<bool> IsThisStudentExistAsync(string studentName, int? studentId)
        {
            if (studentId.HasValue)
            {
                // logic with id
                var studentExists = _genericRepository
                    .GetTableNoTracking()
                    .FirstOrDefaultAsync(s =>
                        s.StudentName.Equals(studentName) & !s.StudentId.Equals(studentId));

                if (studentExists is null)
                    return false;

                return true;
            }
            else
            {
                var studentExists = _genericRepository
                    .GetTableNoTracking()
                    .FirstOrDefaultAsync(s => s.StudentName.Equals(studentName));

                if (studentExists is null)
                    return false;

                return true;
            }
        }
        #endregion
    }
}
