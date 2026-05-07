using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    public partial class Department
    {
        public Department()
        {
            // List makes duplicates possible, but we don't need any dubllications...
            // using HashSet instead of List to prevent duplicates
            Students = new HashSet<Student>();
            DepartmentSubjects = new HashSet<DepartmentSubject>();
        }

        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(32)]
        public string DepartmentName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }
    }
}
