using MediatR;
using SchoolManagementSystem.Core.Features.Students.Queriers.Responses;
using SchoolManagementSystem.Core.Wrappers;

namespace SchoolManagementSystem.Core.Features.Students.Queriers.Models
{
    public class GetAllStudentsPagedQuery : IRequest<PaginatedResult<GetAllStudentsPagedResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string[]? OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
