using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Data.Helpers;

namespace SchoolManagementSystem.Service.Interfaces
{
    public interface IStudentService
    {
        public Task<ICollection<Student>> GetStudentsAsync();
        public IQueryable<Student> GetStudentsPagedQueryable();
        public Task<Student> GetStudentByIdAsync(int id);
        public Task<Student> GetStudentByIdWithoutIncludeDepartmentAsync(int id);
        public Task<string> CreateStudentAsync(Student student);
        public Task<string> DeleteStudentAsync(Student student);
        public Task<string> UpdateStudentAsync(Student student);
        Task<bool> IsThisStudentExistAsync(string studentName, int? studentId = null);
        IQueryable<Student> FilterPagedStudentsQueryable(StudentOrderingEnum? orderBy, string? search);
    }
}
