using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagementSystem.Core.Bases;
using SchoolManagementSystem.Core.Features.Students.Commands.Models;
using SchoolManagementSystem.Core.LocalizationResources;
using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Service.Interfaces;

namespace SchoolManagementSystem.Core.Features.Students.Commands.Handlers
{
    public class StudentsCommandHandler : ResponseHandler,
        IRequestHandler<CreateStudentCommand, Response<string>>,
        IRequestHandler<UpdateStudentCommand, Response<string>>,
        IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public StudentsCommandHandler(IStudentService studentService, IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        #endregion

        #region Handlers
        public async Task<Response<string>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request);

            var response = await _studentService.CreateStudentAsync(student);

            if (response == "created")
                return Created<string>(_stringLocalizer[SharedResourcesKeys.Created]);
            else
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.BadRequest]);
        }

        public async Task<Response<string>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var currentStudent = await _studentService.GetStudentByIdAsync(request.StudentId);

            if (currentStudent is null)
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var student = _mapper.Map<Student>(request);

            var response = await _studentService.UpdateStudentAsync(student);

            if (response == "updated")
                return Created<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
            else
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.BadRequest]);
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var currentStudent = await _studentService.GetStudentByIdWithoutIncludeDepartmentAsync(request.StudentId);

            if (currentStudent is null)
                return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var student = _mapper.Map<Student>(request);

            var response = await _studentService.DeleteStudentAsync(student);

            if (response == "deleted")
                return Deleted<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);
            else
                return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.BadRequest]);
        }
        #endregion
    }
}
