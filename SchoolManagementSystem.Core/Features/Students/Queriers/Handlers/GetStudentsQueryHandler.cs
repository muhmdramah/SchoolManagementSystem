using AutoMapper;
using MediatR;
using SchoolManagementSystem.Core.Bases;
using SchoolManagementSystem.Core.Features.Students.Queriers.Models;
using SchoolManagementSystem.Core.Features.Students.Queriers.Responses;
using SchoolManagementSystem.Service.Interfaces;

namespace SchoolManagementSystem.Core.Features.Students.Queriers.Handlers
{
    public class GetStudentsQueryHandler : ResponseHandler,
        IRequestHandler<GetAllStudentsQuery, Response<ICollection<GetAllStudentsResponse>>>,
        IRequestHandler<GetStudentByIdQuery, Response<GetStudentByIdResponse>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public GetStudentsQueryHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        #endregion

        #region Handlers
        public async Task<Response<ICollection<GetAllStudentsResponse>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentService.GetStudentsAsync();

            if (students is null)
                return NotFound<ICollection<GetAllStudentsResponse>>();

            var response = _mapper.Map<ICollection<GetAllStudentsResponse>>(students);

            return Success(response);
        }

        public async Task<Response<GetStudentByIdResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdAsync(request.Id);

            if (student is null)
                return NotFound<GetStudentByIdResponse>();

            var response = _mapper.Map<GetStudentByIdResponse>(student);

            return Success(response);
        }
        #endregion
    }
}
