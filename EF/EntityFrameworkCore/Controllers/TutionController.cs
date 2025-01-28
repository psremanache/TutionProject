using EntityFrameworkCore.DBConnection;
using EntityFrameworkCore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutionController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public TutionController(DataContext dataContext) { 
            _dataContext = dataContext;
        }

        [Authorize(Roles = "Admin,Instructor,Student")]
        [HttpGet("GetAllCourses")]
        public async Task<IActionResult> GetAllCourses()
        {
            var res = await _dataContext.Courses.ToListAsync();
            return Ok(res);
        }

        [Authorize(Roles = "Admin,Instructor,Student")]
        [HttpGet("GetCoursesById/{id:int}")]
        public async Task<IActionResult> GetAllCourses([FromRoute] int id)
        {
            var res = await _dataContext.Courses.FindAsync(id);//finds by primary key
            return Ok(res);
        }

        [Authorize(Roles = "Student")]
        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var res = await (from student in _dataContext.Students select student).ToListAsync();
            return Ok(res);
        }

        [Authorize(Roles = "Admin,Instructor,Student")]
        [HttpGet("GetStudentById/{id:int}")]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            var res = await _dataContext.Students.Where(x=>x.StudentId == id).FirstOrDefaultAsync();
            //var res = await _dataContext.Students.Where(x => x.StudentId == id).FirstAsync();//breaks if we get null
            return Ok(res);
        }

        [HttpPost("GetStudentInList")]
        public async Task<IActionResult> GetStudentInList([FromBody] List<int> ids)
        {
            var res = await _dataContext.Students.Where(x => ids.Contains(x.StudentId)).ToListAsync();
            //var res = await _dataContext.Students.Where(x => x.StudentId == id).FirstAsync();//breaks if we get null
            return Ok(res);
        }

        [HttpGet("GetStudentByNameId/{name}/{id:int}")]
        public async Task<IActionResult> GetStudentById([FromRoute] string name,[FromRoute] int id)
        {
            var res = await _dataContext.Students.Where(x => x.StudentId == id && x.Name == name).FirstOrDefaultAsync();
            //var res = await _dataContext.Students.Where(x => x.StudentId == id).FirstAsync();//breaks if we get null
            return Ok(res);
        }

        [HttpGet("GetAllStudentEnrolledInCourse/{courseId:int}")]
        public async Task<IActionResult> GetAllStudentEnrolledInCourse([FromRoute] int courseId)
        {
            var studentsInCourse = await _dataContext.Enrollments
             .Where(e => e.CourseId == courseId)
             .Include(e => e.Student) // Include the related Student entity
             .Select(e => e.Student) // Get the student information
             .ToListAsync();
            return Ok(studentsInCourse);
        }
        
        [Authorize(Roles = "Admin,Instructor")]
        [HttpGet("GetAllInstructors")]
        public async Task<IActionResult> GetAllInstructors()
        {
            var res = await _dataContext.Instructors.ToListAsync();
            return Ok(res);
        }

        [Authorize(Roles = "Admin,Instructor")]
        [HttpGet("GetInstructorById/{id:int}")]
        public async Task<IActionResult> GetInstructorById([FromRoute] int id)
        {
            //var res = await _dataContext.Instructors.Where(x => x.InstructorId == id).SingleAsync();// breaks if we get null and if multiple records found
            var res = await _dataContext.Instructors.Where(x => x.InstructorId == id).SingleOrDefaultAsync();//breaks if  multiple records found
            return Ok(res);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllCoursesAndSubsequentStudents")]
        public async Task<IActionResult> GetAllCoursesAndSubsequentStudents()
        {   //some cycle issue is coming in below code
            //var coursesWithStudents = await _dataContext.Courses
            //    .Include(c => c.Enrollments) // Include enrollments for each course
            //        .ThenInclude(e => e.Student) // Include students for each enrollment
            //    .ToListAsync();

            var coursesWithStudents = await _dataContext.Courses
            .Select(c => new
            {
                CourseId = c.CourseId,
                Title = c.Title,
                Students = _dataContext.Enrollments
                    .Where(e => e.CourseId == c.CourseId)
                    .Select(e => new
                    {
                        StudentId = e.StudentId,
                        Name = _dataContext.Students.FirstOrDefault(s => s.StudentId == e.StudentId).Name
                    })
                    .ToList()
            })
            .ToListAsync();

            return Ok(coursesWithStudents);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost("SaveCourse")]
        public async Task<IActionResult> SaveCourse([FromBody] Course course)
        {
            _dataContext.Courses.Add(course);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("SaveEnrollment")]
        public async Task<IActionResult> SaveEnrollment([FromBody] Enrollment enrollment)
        {
            _dataContext.Enrollments.Add(enrollment);
            await _dataContext.SaveChangesAsync();
            return Ok(enrollment);//returns with enrollment id 
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("SaveStudent")]
        public async Task<IActionResult> SaveStudent([FromBody] Student student)
        {
            _dataContext.Students.Add(student);
            await _dataContext.SaveChangesAsync();
            return Ok(student);//returns with student id
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("SaveInstructor")]
        public async Task<IActionResult> SaveInstructor([FromBody] Instructor instructor)
        {
            _dataContext.Instructors.Add(instructor);
            await _dataContext.SaveChangesAsync();
            return Ok(instructor);//returns with instructor id
        }
    }
}
