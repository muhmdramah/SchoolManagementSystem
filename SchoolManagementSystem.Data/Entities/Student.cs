using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Data.Entities
{

    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [StringLength(128)]
        public string StudentName { get; set; }

        [StringLength(46)]
        [Required]
        public string StudentAddress { get; set; }

        [StringLength(16)]
        [Required]
        [Phone]
        public string StudentPhone { get; set; }

        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
    }
}
