using AutoMapper;
using MediatR;
using SchoolManagementSystem.Core.Bases;
using SchoolManagementSystem.Core.Features.Students.Queriers.Models;
using SchoolManagementSystem.Core.Features.Students.Queriers.Responses;
using SchoolManagementSystem.Core.Wrappers;
using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Service.Interfaces;
using System.Linq.Expressions;

namespace SchoolManagementSystem.Core.Features.Students.Queriers.Handlers
{
    public class StudentsQueryHandler : ResponseHandler,
        IRequestHandler<GetAllStudentsQuery, Response<ICollection<GetAllStudentsResponse>>>,
        IRequestHandler<GetStudentByIdQuery, Response<GetStudentByIdResponse>>,
        IRequestHandler<GetAllStudentsPagedQuery, PaginatedResult<GetAllStudentsPagedResponse>>

    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public StudentsQueryHandler(IStudentService studentService, IMapper mapper)
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

        public async Task<PaginatedResult<GetAllStudentsPagedResponse>> Handle(GetAllStudentsPagedQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetAllStudentsPagedResponse>> expression =
                student => new GetAllStudentsPagedResponse(student.StudentId, student.StudentName,
                                student.StudentAddress, student.StudentPhone, student.Department.DepartmentName);

            //var studentsQueryable = _studentService.GetStudentsPagedQueryable();

            var filteredstudentsQueryable = _studentService
                .FilterPagedStudentsQueryable(request.OrderBy, request.Search);

            var paginatedList = await filteredstudentsQueryable
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return paginatedList;
        }
        #endregion
    }
}
