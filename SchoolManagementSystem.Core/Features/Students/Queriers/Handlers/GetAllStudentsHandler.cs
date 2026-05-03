using MediatR;
using SchoolManagementSystem.Core.Features.Students.Queriers.Models;
using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Service.Interfaces;

namespace SchoolManagementSystem.Core.Features.Students.Queriers.Handlers
{
    public class GetAllStudentsHandler : IRequestHandler<GetAllStudentsQuery, ICollection<Student>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        #endregion

        #region Constructors
        public GetAllStudentsHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }
        #endregion

        #region Handlers
        public async Task<ICollection<Student>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            return await _studentService.GetStudentsAsync();
        }
        #endregion
    }
}
