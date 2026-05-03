using AutoMapper;

namespace SchoolManagementSystem.Core.Mapping.StudentMappingProfiles
{
    public partial class StudentProfile : Profile
    {
        public StudentProfile()
        {
            GetAllStudentsMapping();
            GetStudentByIdMapping();
        }
    }
}
