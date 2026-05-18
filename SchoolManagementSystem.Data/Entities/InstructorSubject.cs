using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Data.Entities
{
    public class InstructorSubject
    {
        [Key]
        public int InstructorId { get; set; }

        [Key]
        public int SubjectId { get; set; }

        // Explicit foreign key properties
        [ForeignKey(nameof(InstructorId))]
        public virtual Instructor? Instructor { get; set; }

        [ForeignKey(nameof(SubjectId))]
        public virtual Subject? Subject { get; set; }
    }
}