using MediatR;
using SchoolManagementSystem.Core.Bases;
using SchoolManagementSystem.Core.Features.Students.Queriers.Responses;

namespace SchoolManagementSystem.Core.Features.Students.Queriers.Models
{
    public class GetAllStudentsQuery : IRequest<Response<ICollection<GetAllStudentsResponse>>>
    {
    }
}
