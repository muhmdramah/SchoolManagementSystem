using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchoolManagementSystem.Data.Entities
{
    public class StudentSubject
    {
        [Key]
        public int StudentSubjectId { get; set; }

        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

    }
}
