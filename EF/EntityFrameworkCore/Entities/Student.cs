namespace EntityFrameworkCore.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }

        // Navigation property for many-to-many relationship
        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
