using EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Domain.ServiceInterfaces
{
    public interface ITutionService
    {
        public Task<List<Course>> GetAllCourses();
        public Task<Course?> GetAllCourses(int id);
        public Task<List<Student>> GetAllStudents();
        public Task<Student?> GetStudentById(int id);
        public Task<List<Student>> GetStudentInList(List<int> ids);
        public Task<Student?> GetStudentById(string name, int id);
        public Task<List<Student?>> GetAllStudentEnrolledInCourse(int courseId);
        public Task<List<Instructor>> GetAllInstructors();
        public Task<Instructor?> GetInstructorById(int id);
        public Task<bool> GetAllCoursesAndSubsequentStudents();
        public Task<bool> SaveCourse(Course course);
        public Task<Enrollment> SaveEnrollment(Enrollment enrollment);
        public Task<Student> SaveStudent(Student student);
        public Task<Instructor> SaveInstructor(Instructor instructor);
    }
}
