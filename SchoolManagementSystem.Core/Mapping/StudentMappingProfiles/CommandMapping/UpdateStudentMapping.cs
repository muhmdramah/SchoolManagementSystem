using SchoolManagementSystem.Core.Features.Students.Commands.Models;
using SchoolManagementSystem.Data.Entities;

namespace SchoolManagementSystem.Core.Mapping.StudentMappingProfiles
{
    public partial class StudentProfile
    {
        public void UpdateStudentMapping()
        {
            CreateMap<UpdateStudentCommand, Student>();
        }
    }
}
