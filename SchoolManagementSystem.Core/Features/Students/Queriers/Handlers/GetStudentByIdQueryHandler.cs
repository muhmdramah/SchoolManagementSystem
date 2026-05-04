using AutoMapper;
using MediatR;
using SchoolManagementSystem.Core.Bases;
using SchoolManagementSystem.Core.Features.Students.Queriers.Models;
using SchoolManagementSystem.Core.Features.Students.Queriers.Responses;
using SchoolManagementSystem.Service.Interfaces;

namespace SchoolManagementSystem.Core.Features.Students.Queriers.Handlers
{
    public class GetStudentByIdQueryHandler : ResponseHandler, IRequestHandler<GetStudentByIdQuery, Response<GetStudentByIdResponse>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public GetStudentByIdQueryHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        #endregion

        #region Handlers
        public async Task<Response<GetStudentByIdResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdAsync(request.Id);

            var response = _mapper.Map<GetStudentByIdResponse>(student);

            if (response is null)
                return NotFound<GetStudentByIdResponse>();

            return Success(response);
        }
        #endregion
    }
}
