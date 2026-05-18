using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Data.Entities
{
    [PrimaryKey(nameof(StudentId), nameof(SubjectId))]  // Add this line
    public class StudentSubject
    {
        // Remove the [Key] attributes from these properties
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public decimal? Grade { get; set; }

        [ForeignKey("StudentId")]
        [InverseProperty("StudentSubjects")]
        public virtual Student? Student { get; set; }

        [ForeignKey("SubjectId")]
        [InverseProperty("StudentsSubjects")]
        public virtual Subject? Subject { get; set; }
    }
}