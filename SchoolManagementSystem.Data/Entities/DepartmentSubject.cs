using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Data.Entities
{
    [PrimaryKey(nameof(DepartmentId), nameof(SubjectId))]  // Add this line
    public class DepartmentSubject
    {
        // Remove the [Key] attributes from these properties
        public int DepartmentId { get; set; }
        public int SubjectId { get; set; }

        [ForeignKey("DepartmentId")]
        [InverseProperty("DepartmentSubjects")]
        public virtual Department? Department { get; set; }

        [ForeignKey("SubjectId")]
        [InverseProperty("DepartmetsSubjects")]
        public virtual Subject? Subject { get; set; }
    }
}