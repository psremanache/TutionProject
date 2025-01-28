namespace EntityFrameworkCore.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }

        // Foreign key and navigation property for the instructor
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        // Navigation property for many-to-many relationship
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
