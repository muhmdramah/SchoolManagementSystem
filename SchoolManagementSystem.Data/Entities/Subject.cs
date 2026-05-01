using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolManagementSystem.Data.Entities
{
    public class Subject
    {
        public Subject()
        {
            StudentsSubjects = new HashSet<StudentSubject>();
            DepartmentSubjects = new HashSet<DepartmentSubject>();
        }

        [Key]
        public int SubjectId { get; set; }

        [StringLength(64)]
        [Required]
        public string SubjectName { get; set; }

        [Required]
        public DateTime Period { get; set; }

        public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }
        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }
    }

}
