using EntityFrameworkCore.Domain.RepositoryInterfaces;
using EntityFrameworkCore.Domain.ServiceInterfaces;
using EntityFrameworkCore.Entities;
using EntityFrameworkCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Application
{
    public class TutionService:ITutionService
    {
        private readonly ITutionRepository _tutionRepository;
        public TutionService(ITutionRepository tutionRepository)
        {
            _tutionRepository = tutionRepository;
        }
        public Task<List<Course>> GetAllCourses()
        {
           return _tutionRepository.GetAllCourses();
        }
        public Task<Course?> GetAllCourses(int id)
        {
            return _tutionRepository.GetAllCourses(id);
        }

        public Task<List<Student>> GetAllStudents()
        {
            return _tutionRepository.GetAllStudents();
        }
        public Task<Student?> GetStudentById(int id)
        {
            return _tutionRepository.GetStudentById(id);
        }
        public Task<List<Student>> GetStudentInList(List<int> ids)
        {
            return _tutionRepository.GetStudentInList(ids);
        }
        public Task<Student?> GetStudentById(string name, int id)
        {
            return _tutionRepository.GetStudentById(name,id);
        }
        public Task<List<Student?>> GetAllStudentEnrolledInCourse(int courseId)
        {
            return _tutionRepository.GetAllStudentEnrolledInCourse(courseId);
        }
        public Task<List<Instructor>> GetAllInstructors()
        {
            return _tutionRepository.GetAllInstructors();
        }
        public Task<Instructor?> GetInstructorById(int id)
        {
            return _tutionRepository.GetInstructorById(id);
        }
        public Task<bool> GetAllCoursesAndSubsequentStudents()
        {
            return _tutionRepository.GetAllCoursesAndSubsequentStudents();
        }
        public Task<bool> SaveCourse(Course course)
        {
            return _tutionRepository.SaveCourse(course);
        }
        public Task<Enrollment> SaveEnrollment(Enrollment enrollment)
        {
            return _tutionRepository.SaveEnrollment(enrollment);
        }
        public Task<Student> SaveStudent(Student student)
        {
            return _tutionRepository.SaveStudent(student);
        }
        public Task<Instructor> SaveInstructor(Instructor instructor)
        {
            return _tutionRepository.SaveInstructor(instructor);
        }
    }
}
