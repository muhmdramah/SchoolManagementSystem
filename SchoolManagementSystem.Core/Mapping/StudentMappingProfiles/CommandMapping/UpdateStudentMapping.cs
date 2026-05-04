using SchoolManagementSystem.Core.Features.Students.Commands.Responses;
using SchoolManagementSystem.Data.Entities;

namespace SchoolManagementSystem.Core.Mapping.StudentMappingProfiles
{
    public partial class StudentProfile
    {
        public void GetUpdateStudentMapping()
        {
            CreateMap<Student, UpdateStudentResponse>()
                .ForMember(dest => dest.DepartmentName,
                    opt => opt.MapFrom(src => src.Department.DepartmentName));
        }
    }
}
