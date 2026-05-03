using MediatR;
using SchoolManagementSystem.Core.Bases;
using SchoolManagementSystem.Core.Features.Students.Queriers.Responses;

namespace SchoolManagementSystem.Core.Features.Students.Queriers.Models
{
    public class GetStudentByIdQuery : IRequest<Response<GetStudentByIdResponse>>
    {
        #region Fields
        private int _id;
        #endregion

        #region Constructor
        public GetStudentByIdQuery(int id)
        {
            _id = id;
        }
        #endregion

        #region Properties
        public int Id => _id;
        #endregion
    }
}
