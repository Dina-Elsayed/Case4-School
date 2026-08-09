using Case4_School.DTOs;
using Case4_School.Models;
using Case4_School.Repos.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Case4_School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        public TeachersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _unitOfWork.Teachers.GetAllAsync();
            if (teachers == null || !teachers.Any())
            {
                return NotFound("No teachers found.");
            }
            var result = teachers.Select(t => new ReadTeacherDto
            {
                Id = t.Id,
                FullName = t.FullName,
                Email = t.Email,
                Phone = t.Phone,
                HireDate = t.HireDate
            });

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create(TeacherDto dto)
        {
            var teacher = new Teacher
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                HireDate = dto.HireDate
            };

            await _unitOfWork.Teachers.AddAsync(teacher);

            await _unitOfWork.CompleteAsync();

            var result = new ReadTeacherDto
            {
                Id = teacher.Id,
                FullName = teacher.FullName,
                Email = teacher.Email,
                Phone = teacher.Phone,
                HireDate = teacher.HireDate
            };

            return Ok(result);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            TeacherDto dto)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);

            if (teacher == null)
                return NotFound("Teacher not found.");

            teacher.FullName = dto.FullName;
            teacher.Email = dto.Email;
            teacher.Phone = dto.Phone;
            teacher.HireDate = dto.HireDate;

            _unitOfWork.Teachers.Update(teacher);

            await _unitOfWork.CompleteAsync();

            var result = new ReadTeacherDto
            {
                Id = teacher.Id,
                FullName = teacher.FullName,
                Email = teacher.Email,
                Phone = teacher.Phone,
                HireDate = teacher.HireDate
            };

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);

            if (teacher == null)
                return NotFound("Teacher not found.");


            bool hasCourses =
                await _unitOfWork.Teachers.HasCoursesAsync(id);

            if (hasCourses)
            {
                return BadRequest("Cannot delete a teacher who has courses.");
            }

            await _unitOfWork.Teachers.DeleteAsync(id);

            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
