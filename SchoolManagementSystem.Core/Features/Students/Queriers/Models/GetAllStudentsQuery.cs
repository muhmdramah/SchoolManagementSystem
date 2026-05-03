using MediatR;
using SchoolManagementSystem.Core.Features.Students.Queriers.Responses;

namespace SchoolManagementSystem.Core.Features.Students.Queriers.Models
{
    public class GetAllStudentsQuery : IRequest<ICollection<GetAllStudentsResponse>>
    {
    }
}
