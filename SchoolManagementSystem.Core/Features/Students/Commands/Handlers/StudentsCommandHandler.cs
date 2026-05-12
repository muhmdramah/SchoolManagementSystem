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
                return Created<string>($"Student with name *{request.StudentName}* created successfully!");
            else
                return BadRequest<string>("Failed to create the student.");
        }

        public async Task<Response<string>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var currentStudent = await _studentService.GetStudentByIdAsync(request.StudentId);

            if (currentStudent is null)
                return NotFound<string>("Student not found.");

            var student = _mapper.Map<Student>(request);

            var response = await _studentService.UpdateStudentAsync(student);

            if (response == "updated")
                return Created<string>($"Student with id: {request.StudentId} was updated successfully!");
            else
                return BadRequest<string>("Failed to update the student.");
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var currentStudent = await _studentService.GetStudentByIdWithoutIncludeDepartmentAsync(request.StudentId);

            if (currentStudent is null)
                return NotFound<string>("Student not found.");

            var student = _mapper.Map<Student>(request);

            var response = await _studentService.DeleteStudentAsync(student);

            if (response == "deleted")
                return Deleted<string>($"Student with id: {request.StudentId} was deleted successfully!");
            else
                return BadRequest<string>("Failed to delete the student.");
        }
        #endregion
    }
}
