using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Data.Entities
{
    public class Instructor
    {
        public Instructor()
        {
            Instructors = new HashSet<Instructor>();
            InstructorSubjects = new HashSet<InstructorSubject>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InstructorId { get; set; }

        public string? InstructorName { get; set; }
        public string? InstructorAddress { get; set; }
        public string? InstructorPosition { get; set; }
        public decimal? InstructorSalary { get; set; }
        public string? InstructorImage { get; set; }

        // Foreign keys - make them explicit
        public int? SupervisorId { get; set; }
        public int DepartmentId { get; set; }

        // Department relationship
        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty("Instructors")]
        public Department? Department { get; set; }

        // Department Manager relationship (one-to-one)
        [InverseProperty("Instructor")]
        public Department? DepartmentManager { get; set; }

        // Self-referencing Supervisor relationship
        [ForeignKey(nameof(SupervisorId))]
        [InverseProperty("Instructors")]
        public Instructor? Supervisor { get; set; }

        [InverseProperty("Supervisor")]
        public virtual ICollection<Instructor> Instructors { get; set; }

        // InstructorSubjects relationship
        [InverseProperty("Instructor")]
        public virtual ICollection<InstructorSubject> InstructorSubjects { get; set; }
    }
}