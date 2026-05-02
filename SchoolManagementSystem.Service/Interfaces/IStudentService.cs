using SchoolManagementSystem.Data.Entities;

namespace SchoolManagementSystem.Service.Interfaces
{
    public interface IStudentService
    {
        public Task<ICollection<Student>> GetStudentsAsync();
        public Task<Student> GetStudentByIdAsync(int id);
    }
}
