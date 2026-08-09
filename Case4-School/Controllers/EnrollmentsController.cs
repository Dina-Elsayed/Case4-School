using Case4_School.DTOs;
using Case4_School.Models;
using Case4_School.Repos.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Case4_School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public EnrollmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Create(EnrollmentDto dto)
        {
            var student = await _unitOfWork.Students
                .GetByIdAsync(dto.StudentId);

            if (student == null)
                return NotFound("Student not found.");

            var course = await _unitOfWork.Courses
                .GetByIdAsync(dto.CourseId);

            if (course == null)
                return NotFound("Course not found.");

            bool alreadyEnrolled =
                await _unitOfWork.Enrollments
                    .IsStudentEnrolledAsync(
                        dto.StudentId,
                        dto.CourseId);

            if (alreadyEnrolled)
                return BadRequest(
                    "Student is already enrolled in this course.");

            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                EnrollmentDate = dto.EnrollmentDate,
                Grade = dto.Grade
            };

            await _unitOfWork.Enrollments.AddAsync(enrollment);

            await _unitOfWork.CompleteAsync();

            var result = new ReadEnrollmentDto
            {
                Id = enrollment.Id,
                StudentName = student.FullName,
                CourseTitle = course.Title,
                EnrollmentDate = enrollment.EnrollmentDate,
                Grade = enrollment.Grade
            };

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var enrollment =
                await _unitOfWork.Enrollments.GetByIdAsync(id);

            if (enrollment == null)
                return NotFound("Enrollment not found.");

            await _unitOfWork.Enrollments.DeleteAsync(id);

            await _unitOfWork.CompleteAsync();

            return NoContent();
        }


        [HttpGet("course/{id}")]
        public async Task<IActionResult> GetByCourse(int id)
        {
            var course =
                await _unitOfWork.Courses.GetByIdAsync(id);

            if (course == null)
                return NotFound("Course not found.");

            var enrollments =
                await _unitOfWork.Enrollments.GetByCourseAsync(id);

            var result = enrollments.Select(e => new ReadEnrollmentDto
            {
                Id = e.Id,
                StudentName = e.Student?.FullName ?? "Unknown",
                CourseTitle = e.Course?.Title ?? "Unknown",
                EnrollmentDate = e.EnrollmentDate,
                Grade = e.Grade
            });

            return Ok(result);
        }
    }
}
