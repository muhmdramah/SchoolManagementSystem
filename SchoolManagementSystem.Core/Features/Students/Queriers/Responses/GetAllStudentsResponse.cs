namespace SchoolManagementSystem.Core.Features.Students.Queriers.Responses
{
    public class GetAllStudentsResponse
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentAddress { get; set; }
        public string StudentPhone { get; set; }
        public string DepartmentName { get; set; }
    }
}
