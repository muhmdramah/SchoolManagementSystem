using MediatR;
using SchoolManagementSystem.Core.Features.Students.Queriers.Models;
using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Service.Interfaces;

namespace SchoolManagementSystem.Core.Features.Students.Queriers.Handlers
{
    public class GetStudentByIdHandler : IRequestHandler<GetStudentByIdQuery, Student>
    {
        #region Fields
        private readonly IStudentService _studentService;
        #endregion

        #region Constructors
        public GetStudentByIdHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }
        #endregion

        #region Handlers
        public async Task<Student> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _studentService.GetStudentByIdAsync(request.Id);
            return result;
        }
        #endregion
    }
}
