using EntityFrameworkCore.Domain.ServiceInterfaces;
using EntityFrameworkCore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutionController : ControllerBase
    {
        private readonly ITutionService _tutionService;
        public TutionController(ITutionService tutionService) { 
            _tutionService = tutionService;
        }

        [Authorize(Roles = "Admin,Instructor,Student")]
        [HttpGet("GetAllCourses")]
        public async Task<IActionResult> GetAllCourses()
        {
            var res = await _tutionService.GetAllCourses();
            return Ok(res);
        }

        [Authorize(Roles = "Admin,Instructor,Student")]
        [HttpGet("GetCoursesById/{id:int}")]
        public async Task<IActionResult> GetAllCourses([FromRoute] int id)
        {
            var res = await _tutionService.GetAllCourses(id);//finds by primary key
            return Ok(res);
        }

        [Authorize(Roles = "Student")]
        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            var res = await _tutionService.GetAllStudents();
            return Ok(res);
        }

        [Authorize(Policy = "AllPolicy")]
        [HttpGet("GetStudentById/{id:int}")]
        public async Task<IActionResult> GetStudentById([FromRoute] int id)
        {
            var res = await _tutionService.GetStudentById(id);
            //var res = await _dataContext.Students.Where(x => x.StudentId == id).FirstAsync();//breaks if we get null
            return Ok(res);
        }

        [HttpPost("GetStudentInList")]
        public async Task<IActionResult> GetStudentInList([FromBody] List<int> ids)
        {
            var res = await _tutionService.GetStudentInList(ids);
            //var res = await _dataContext.Students.Where(x => x.StudentId == id).FirstAsync();//breaks if we get null
            return Ok(res);
        }

        [HttpGet("GetStudentByNameId/{name}/{id:int}")]
        public async Task<IActionResult> GetStudentById([FromRoute] string name,[FromRoute] int id)
        {
            var res = await _tutionService.GetStudentById(name, id);
            //var res = await _dataContext.Students.Where(x => x.StudentId == id).FirstAsync();//breaks if we get null
            return Ok(res);
        }

        [HttpGet("GetAllStudentEnrolledInCourse/{courseId:int}")]
        public async Task<IActionResult> GetAllStudentEnrolledInCourse([FromRoute] int courseId)
        {
            var studentsInCourse = await _tutionService.GetAllStudentEnrolledInCourse(courseId);
            return Ok(studentsInCourse);
        }
        
        [Authorize(Roles = "Admin,Instructor")]
        [HttpGet("GetAllInstructors")]
        public async Task<IActionResult> GetAllInstructors()
        {
            var res = await _tutionService.GetAllInstructors();
            return Ok(res);
        }

        [Authorize(Roles = "Admin,Instructor")]
        [HttpGet("GetInstructorById/{id:int}")]
        public async Task<IActionResult> GetInstructorById([FromRoute] int id)
        {
            var res = await _tutionService.GetInstructorById(id);
            return Ok(res);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllCoursesAndSubsequentStudents")]
        public async Task<IActionResult> GetAllCoursesAndSubsequentStudents()
        {
            var coursesWithStudents = await _tutionService.GetAllCoursesAndSubsequentStudents();
            return Ok(coursesWithStudents);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost("SaveCourse")]
        public async Task<IActionResult> SaveCourse([FromBody] Course course)
        {
            await _tutionService.SaveCourse(course);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("SaveEnrollment")]
        public async Task<IActionResult> SaveEnrollment([FromBody] Enrollment enrollment)
        {
           await _tutionService.SaveEnrollment(enrollment);
            return Ok(enrollment);//returns with enrollment id 
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("SaveStudent")]
        public async Task<IActionResult> SaveStudent([FromBody] Student student)
        {
            await _tutionService.SaveStudent(student);
            return Ok(student);//returns with student id
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("SaveInstructor")]
        public async Task<IActionResult> SaveInstructor([FromBody] Instructor instructor)
        {
            await _tutionService.SaveInstructor(instructor);
            return Ok(instructor);//returns with instructor id
        }
    }
}
