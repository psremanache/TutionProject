using EntityFrameworkCore.Domain.RepositoryInterfaces;
using EntityFrameworkCore.Entities;
using EntityFrameworkCore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Infrastructure.Repositories
{
    public class TutionRepository : ITutionRepository
    {
        private readonly DataContext _dataContext;
        public TutionRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Course>> GetAllCourses()
        {
            var res = await _dataContext.Courses.ToListAsync();
            return res;
        }

        public async Task<Course?> GetAllCourses(int id)
        {
            var res = await _dataContext.Courses.FindAsync(id);//finds by primary key
            return res;
        }

        public async Task<List<Student>> GetAllStudents()
        {
            var res = await (from student in _dataContext.Students select student).ToListAsync();
            return res;
        }

        public async Task<Student?> GetStudentById(int id)
        {
            var res = await _dataContext.Students.Where(x => x.StudentId == id).FirstOrDefaultAsync();
            //var res = await _dataContext.Students.Where(x => x.StudentId == id).FirstAsync();//breaks if we get null
            return res;
        }

        public async Task<List<Student>> GetStudentInList(List<int> ids)
        {
            var res = await _dataContext.Students.Where(x => ids.Contains(x.StudentId)).ToListAsync();
            //var res = await _dataContext.Students.Where(x => x.StudentId == id).FirstAsync();//breaks if we get null
            return res;
        }

        public async Task<Student?> GetStudentById(string name,int id)
        {
            var res = await _dataContext.Students.Where(x => x.StudentId == id && x.Name == name).FirstOrDefaultAsync();
            //var res = await _dataContext.Students.Where(x => x.StudentId == id).FirstAsync();//breaks if we get null
            return res;
        }

        public async Task<List<Student?>> GetAllStudentEnrolledInCourse(int courseId)
        {
            var studentsInCourse = await _dataContext.Enrollments
             .Where(e => e.CourseId == courseId)
             .Include(e => e.Student) // Include the related Student entity
             .Select(e => e.Student) // Get the student information
             .ToListAsync();
            return studentsInCourse;
        }

        public async Task<List<Instructor>> GetAllInstructors()
        {
            var res = await _dataContext.Instructors.ToListAsync();
            return res;
        }

        public async Task<Instructor?> GetInstructorById(int id)
        {
            //var res = await _dataContext.Instructors.Where(x => x.InstructorId == id).SingleAsync();// breaks if we get null and if multiple records found
            var res = await _dataContext.Instructors.Where(x => x.InstructorId == id).SingleOrDefaultAsync();//breaks if  multiple records found
            return res;
        }

        public async Task<bool> GetAllCoursesAndSubsequentStudents()
        {   //some cycle issue is coming in below code
            //var coursesWithStudents = await _dataContext.Courses
            //    .Include(c => c.Enrollments) // Include enrollments for each course
            //        .ThenInclude(e => e.Student) // Include students for each enrollment
            //    .ToListAsync();

            //var coursesWithStudents = await _dataContext.Courses
            //.Select(c => new
            //{
            //    CourseId = c.CourseId,
            //    Title = c.Title,
            //    Students = _dataContext.Enrollments
            //        .Where(e => e.CourseId == c.CourseId)
            //        .Select(e => new
            //        {
            //            StudentId = e.StudentId,
            //            Name = _dataContext.Students.FirstOrDefault(s => s.StudentId == e.StudentId).Name
            //        })
            //        .ToList()
            //})
            //.ToListAsync();

            //return coursesWithStudents;
            return true;
        }

        public async Task<bool> SaveCourse(Course course)
        {
            _dataContext.Courses.Add(course);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<Enrollment> SaveEnrollment(Enrollment enrollment)
        {
            _dataContext.Enrollments.Add(enrollment);
            await _dataContext.SaveChangesAsync();
            return enrollment;//returns with enrollment id 
        }

        public async Task<Student> SaveStudent(Student student)
        {
            _dataContext.Students.Add(student);
            await _dataContext.SaveChangesAsync();
            return student;//returns with student id
        }

        public async Task<Instructor> SaveInstructor(Instructor instructor)
        {
            _dataContext.Instructors.Add(instructor);
            await _dataContext.SaveChangesAsync();
            return instructor;//returns with instructor id
        }
    }
}
