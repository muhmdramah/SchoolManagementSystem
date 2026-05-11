namespace SchoolManagementSystem.Core.Features.Students.Queriers.Responses
{
    public class GetAllStudentsPagedResponse
    {
        public GetAllStudentsPagedResponse(int studentId, string studentName, string studentAddress, string studentPhone, string departmentName)
        {
            StudentId = studentId;
            StudentName = studentName;
            StudentAddress = studentAddress;
            StudentPhone = studentPhone;
            DepartmentName = departmentName;
        }

        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentAddress { get; set; }
        public string StudentPhone { get; set; }
        public string DepartmentName { get; set; }
    }
}
