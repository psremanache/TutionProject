using EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //fluent api for complex relations
            modelBuilder.Entity<Enrollment>()
            .HasKey(e => new { e.StudentId, e.CourseId }); // Composite Key

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);

            // Configure one-to-many relationship between Instructor and Course
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorId);

            modelBuilder.Entity<User>().
                 HasOne(r => r.Role)
                 .WithMany(r => r.Users)
                 .HasForeignKey(r => r.RoleId);

            modelBuilder.Entity<Student>()
            .HasIndex(s => s.Name)
            .IsUnique();

            modelBuilder.Entity<Course>()
            .HasIndex(s => s.Title)
            .IsUnique();

            modelBuilder.Entity<Instructor>()
            .HasIndex(s => s.FullName)
            .IsUnique();



            modelBuilder.Entity<Instructor>().HasData(
            new Instructor { InstructorId = 1, FullName = "Dr. John Smith" },
            new Instructor { InstructorId = 2, FullName = "Dr. Emily Johnson" },
            new Instructor { InstructorId = 3, FullName = "Prof. Michael Brown" },
            new Instructor { InstructorId = 4, FullName = "Dr. Sarah Davis" }
        );

            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 1, Name = "Alice Johnson" },
                new Student { StudentId = 2, Name = "Bob Smith" },
                new Student { StudentId = 3, Name = "Charlie Brown" },
                new Student { StudentId = 4, Name = "Diana Ross" }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 1, Title = "Mathematics 101", InstructorId = 1 },
                new Course { CourseId = 2, Title = "Physics 101", InstructorId = 2 },
                new Course { CourseId = 3, Title = "Chemistry 101", InstructorId = 3 },
                new Course { CourseId = 4, Title = "Biology 101", InstructorId = 4 }
            );

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { StudentId = 1, CourseId = 1, EnrollmentDate = new DateTime(2023, 1, 15) },
                new Enrollment { StudentId = 2, CourseId = 2, EnrollmentDate = new DateTime(2023, 1, 16) },
                new Enrollment { StudentId = 3, CourseId = 3, EnrollmentDate = new DateTime(2023, 1, 17) },
                new Enrollment { StudentId = 4, CourseId = 4, EnrollmentDate = new DateTime(2023, 1, 18) }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Admin" },
                new Role { RoleId = 2, RoleName = "Student" },
                new Role { RoleId = 3, RoleName = "Instructor" }
                );
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
