using Case4_School.DTOs;
using Case4_School.Models;
using Case4_School.Repos.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Case4_School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public CoursesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _unitOfWork.Courses.GetAllWithTeacherAsync();
       

            var result = courses.Select(c => new ReadCourseDto
            {
                Id = c.Id,
                Title = c.Title,
                CreditHours = c.CreditHours,
                TeacherName = c.Teacher?.FullName ?? "No Teacher"
            });

            return Ok(result);
        }



        [HttpPost]
        public async Task<IActionResult> Create(CourseDto dto)
        {
            var teacher = await _unitOfWork.Teachers
                .GetByIdAsync(dto.TeacherId);

            if (teacher == null)
                return NotFound("Teacher not found.");

            var course = new Course
            {
                Title = dto.Title,
                CreditHours = dto.CreditHours,
                TeacherId = dto.TeacherId
            };

            await _unitOfWork.Courses.AddAsync(course);

            await _unitOfWork.CompleteAsync();

            var result = new ReadCourseDto
            {
                Id = course.Id,
                Title = course.Title,
                CreditHours = course.CreditHours,
                TeacherName = teacher.FullName
            };

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _unitOfWork.Courses.GetCourseWithTeacherAsync(id);

            if (course == null)
                return NotFound("Course not found.");

            var result = new ReadCourseDto
            {
                Id = course.Id,
                Title = course.Title,
                CreditHours = course.CreditHours,
                TeacherName = course.Teacher?.FullName ?? "No Teacher"
            };

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,CourseDto dto)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);

            if (course == null)
                return NotFound("Course not found.");

            var teacher = await _unitOfWork.Teachers
                .GetByIdAsync(dto.TeacherId);

            if (teacher == null)
                return NotFound("Teacher not found.");

            course.Title = dto.Title;
            course.CreditHours = dto.CreditHours;
            course.TeacherId = dto.TeacherId;

            _unitOfWork.Courses.Update(course);

            await _unitOfWork.CompleteAsync();

            var result = new ReadCourseDto
            {
                Id = course.Id,
                Title = course.Title,
                CreditHours = course.CreditHours,
                TeacherName = teacher.FullName
            };

            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);

            if (course == null)
                return NotFound("Course not found.");

            bool hasStudents =
                await _unitOfWork.Courses.HasStudentsAsync(id);

            if (hasStudents)
                return BadRequest(
                    "Cannot delete a course that has students.");

            await _unitOfWork.Courses.DeleteAsync(id);

            await _unitOfWork.CompleteAsync();

            return NoContent();
        }


        [HttpGet("search")]
        public async Task<IActionResult> Search(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest("Title is required.");

            var courses =
                await _unitOfWork.Courses.SearchByTitleAsync(title);

            var result = courses.Select(c => new ReadCourseDto
            {
                Id = c.Id,
                Title = c.Title,
                CreditHours = c.CreditHours,
                TeacherName = c.Teacher?.FullName ?? "No Teacher"
            });

            return Ok(result);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter(int? teacherId, int? creditHours)
        {
            var courses =
                await _unitOfWork.Courses.FilterCoursesAsync(
                    teacherId,
                    creditHours);

            var result = courses.Select(c => new ReadCourseDto
            {
                Id = c.Id,
                Title = c.Title,
                CreditHours = c.CreditHours,
                TeacherName = c.Teacher?.FullName ?? "No Teacher"
            });

            return Ok(result);
        }


        [HttpGet("{id}/has-students")]
        public async Task<IActionResult> HasStudents(int id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);

            if (course == null)
                return NotFound("Course not found.");

            bool result =
                await _unitOfWork.Courses.HasStudentsAsync(id);

            return Ok(result);
        }
    }
}
