namespace EntityFrameworkCore.Entities
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string FullName { get; set; }

        // Navigation property for courses taught by the instructor
        public ICollection<Course> Courses { get; set; }
    }
}
