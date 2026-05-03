using MediatR;
using SchoolManagementSystem.Data.Entities;

namespace SchoolManagementSystem.Core.Features.Students.Queriers.Models
{
    public class GetAllStudentsQuery : IRequest<ICollection<Student>>
    {
    }
}
