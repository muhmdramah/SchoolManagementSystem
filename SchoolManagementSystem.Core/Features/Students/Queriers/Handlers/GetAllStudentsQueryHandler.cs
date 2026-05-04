using AutoMapper;
using MediatR;
using SchoolManagementSystem.Core.Bases;
using SchoolManagementSystem.Core.Features.Students.Queriers.Models;
using SchoolManagementSystem.Core.Features.Students.Queriers.Responses;
using SchoolManagementSystem.Service.Interfaces;

namespace SchoolManagementSystem.Core.Features.Students.Queriers.Handlers
{
    public class GetAllStudentsQueryHandler : ResponseHandler, IRequestHandler<GetAllStudentsQuery, Response<ICollection<GetAllStudentsResponse>>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public GetAllStudentsQueryHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        #endregion

        #region Handlers
        public async Task<Response<ICollection<GetAllStudentsResponse>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentService.GetStudentsAsync();

            var response = _mapper.Map<ICollection<GetAllStudentsResponse>>(students);

            if (response is null)
                return NotFound<ICollection<GetAllStudentsResponse>>();

            return Success(response);
        }
        #endregion

    }
}
