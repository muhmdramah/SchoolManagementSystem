using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data.Entities;

namespace SchoolManagementSystem.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<DepartmentSubject> DepartmentSubjects { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<InstructorSubject> InstructorSubjects { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Decimal precision configurations
            modelBuilder.Entity<Instructor>()
                .Property(i => i.InstructorSalary)
                .HasPrecision(18, 2);

            modelBuilder.Entity<StudentSubject>()
                .Property(ss => ss.Grade)
                .HasPrecision(5, 2);

            // Composite primary keys
            modelBuilder.Entity<DepartmentSubject>()
                .HasKey(ds => new { ds.DepartmentId, ds.SubjectId });

            modelBuilder.Entity<InstructorSubject>()
                .HasKey(iss => new { iss.InstructorId, iss.SubjectId });

            modelBuilder.Entity<StudentSubject>()
                .HasKey(ss => new { ss.StudentId, ss.SubjectId });

            // RELATIONSHIPS - Modified to avoid cascade cycles

            // Instructor - Department (CHANGE to Restrict or NoAction)
            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.Department)
                .WithMany(d => d.Instructors)
                .HasForeignKey(i => i.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);  // Changed from Cascade

            // Department - Instructor Manager (keep as SetNull - this is fine)
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Instructor)
                .WithOne(i => i.DepartmentManager)
                .HasForeignKey<Department>(d => d.InstructorManager)
                .OnDelete(DeleteBehavior.SetNull);

            // Instructor - Supervisor self-reference (keep as Restrict)
            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.Supervisor)
                .WithMany(i => i.Instructors)
                .HasForeignKey(i => i.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Instructor - InstructorSubject (keep as Cascade - this is fine)
            modelBuilder.Entity<InstructorSubject>()
                .HasOne(iss => iss.Instructor)
                .WithMany(i => i.InstructorSubjects)
                .HasForeignKey(iss => iss.InstructorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Subject - InstructorSubject (use Restrict)
            modelBuilder.Entity<InstructorSubject>()
                .HasOne(iss => iss.Subject)
                .WithMany(s => s.InstructorSubjects)
                .HasForeignKey(iss => iss.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // Department - DepartmentSubject (use Restrict)
            modelBuilder.Entity<DepartmentSubject>()
                .HasOne(ds => ds.Department)
                .WithMany(d => d.DepartmentSubjects)
                .HasForeignKey(ds => ds.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Subject - DepartmentSubject (use Restrict)
            modelBuilder.Entity<DepartmentSubject>()
                .HasOne(ds => ds.Subject)
                .WithMany(s => s.DepartmetsSubjects)
                .HasForeignKey(ds => ds.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // Student - Department (use SetNull)
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            // Student - StudentSubject (use Restrict)
            modelBuilder.Entity<StudentSubject>()
                .HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Subject - StudentSubject (use Restrict)
            modelBuilder.Entity<StudentSubject>()
                .HasOne(ss => ss.Subject)
                .WithMany(s => s.StudentsSubjects)
                .HasForeignKey(ss => ss.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
