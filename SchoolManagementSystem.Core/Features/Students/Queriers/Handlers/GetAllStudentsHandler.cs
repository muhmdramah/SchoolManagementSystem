using AutoMapper;
using MediatR;
using SchoolManagementSystem.Core.Features.Students.Queriers.Models;
using SchoolManagementSystem.Core.Features.Students.Queriers.Responses;
using SchoolManagementSystem.Service.Interfaces;

namespace SchoolManagementSystem.Core.Features.Students.Queriers.Handlers
{
    public class GetAllStudentsHandler : IRequestHandler<GetAllStudentsQuery, ICollection<GetAllStudentsResponse>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public GetAllStudentsHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        #endregion

        #region Handlers
        public async Task<ICollection<GetAllStudentsResponse>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentService.GetStudentsAsync();

            var response = _mapper.Map<ICollection<GetAllStudentsResponse>>(students);

            return response;
        }
        #endregion

    }
}
