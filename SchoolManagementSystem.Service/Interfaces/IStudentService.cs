using SchoolManagementSystem.Data.Entities;

namespace SchoolManagementSystem.Service.Interfaces
{
    public interface IStudentService
    {
        public Task<ICollection<Student>> GetStudentsAsync();
        public Task<Student> GetStudentByIdAsync(int id);
        public Task<string> AddStudentAsync(Student student);
        public Task DeleteStudentAsync(Student student);
        public Task<string> UpdateStudentAsync(Student student);
        Task<bool> IsThisStudentExistAsync(string studentName, int? studentId = null);
    }
}
