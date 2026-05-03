using SchoolManagementSystem.Core.Features.Students.Queriers.Responses;
using SchoolManagementSystem.Data.Entities;

namespace SchoolManagementSystem.Core.Mapping.StudentMappingProfiles
{
    public partial class StudentProfile
    {
        public void GetAllStudentsMapping()
        {
            CreateMap<Student, GetAllStudentsResponse>()
                .ForMember(dest => dest.DepartmentName,
                    opt => opt.MapFrom(src => src.Department.DepartmentName));
        }
    }
}
