using AutoMapper;
using MediatR;
using SchoolManagementSystem.Core.Bases;
using SchoolManagementSystem.Core.Features.Students.Commands.Models;
using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Service.Interfaces;

namespace SchoolManagementSystem.Core.Features.Students.Commands.Handlers
{
    public class StudentsCommandHandler : ResponseHandler,
        IRequestHandler<CreateStudentCommand, Response<string>>,
        IRequestHandler<UpdateStudentCommand, Response<string>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public StudentsCommandHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        #endregion

        #region Handlers
        public async Task<Response<string>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request);

            var response = await _studentService.AddStudentAsync(student);

            if (response == "exists")
                return UnprocessableEntity<string>("Failed to create the student.");

            else if (response == "created")
                return Created<string>("Student created successfully!");
            else
                return BadRequest<string>("Failed to create the student.");
        }

        public async Task<Response<string>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(request);

            var response = await _studentService.UpdateStudentAsync(student);

            if (response == "updated")
                return Created<string>("Student created successfully!");
            else
                return BadRequest<string>("Failed to create the student.");
        }
        #endregion
    }
}
