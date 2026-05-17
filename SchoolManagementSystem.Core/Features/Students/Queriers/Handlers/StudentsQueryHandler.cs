using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagementSystem.Core.Bases;
using SchoolManagementSystem.Core.Features.Students.Queriers.Models;
using SchoolManagementSystem.Core.Features.Students.Queriers.Responses;
using SchoolManagementSystem.Core.LocalizationResources;
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
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        #endregion

        #region Constructors
        public StudentsQueryHandler(IStudentService studentService, IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        #endregion

        #region Handlers
        public async Task<Response<ICollection<GetAllStudentsResponse>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentService.GetStudentsAsync();

            if (students is null)
                return NotFound<ICollection<GetAllStudentsResponse>>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var studentsCollection = _mapper.Map<ICollection<GetAllStudentsResponse>>(students);

            var response = Success(studentsCollection);

            response.Meta = new
            {
                TotalCount = $"{studentsCollection.Count} students exist!"
            };

            return response;
        }

        public async Task<Response<GetStudentByIdResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdAsync(request.Id);

            if (student is null)
                return NotFound<GetStudentByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var response = _mapper.Map<GetStudentByIdResponse>(student);

            return Success(response);
        }

        public async Task<PaginatedResult<GetAllStudentsPagedResponse>> Handle(GetAllStudentsPagedQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetAllStudentsPagedResponse>> expression =
                student => new GetAllStudentsPagedResponse(student.StudentId, student.StudentName,
                                student.StudentAddress, student.StudentPhone, student.Department.DepartmentName);

            var filteredstudentsQueryable = _studentService
                .FilterPagedStudentsQueryable(request.OrderBy, request.Search);

            var paginatedList = await filteredstudentsQueryable
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            paginatedList.Meta = new
            {
                TotalCount = $"{paginatedList.Data.Count} students exist just in this page!"
            };

            return paginatedList;
        }
        #endregion
    }
}
