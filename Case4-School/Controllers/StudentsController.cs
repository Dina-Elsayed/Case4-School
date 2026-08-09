using Case4_School.DTOs;
using Case4_School.Models;
using Case4_School.Repos.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Case4_School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public StudentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpPost]
        public async Task<IActionResult> Create(StudentDto dto)
        {
            if(dto == null)
            {
                return BadRequest("Student data is required.");
            }
            var student = new Student
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender
            };

            await _unitOfWork.Students.AddAsync(student);

            await _unitOfWork.CompleteAsync();

            var result = new ReadStudentDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                Phone = student.Phone,
                BirthDate = student.BirthDate,
                Gender = student.Gender
            };

            return Ok(result);
        }


        [HttpGet("Paged")]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                return BadRequest("Page number and page size must be greater than zero.");

            var students = await _unitOfWork.Students.GetPagedAsync(pageNumber, pageSize);
            var result = students.Select(s => new ReadStudentDto
            {
                Id = s.Id,
                FullName = s.FullName,
                Email = s.Email,
                Phone = s.Phone,
                BirthDate = s.BirthDate,
                Gender = s.Gender
            });
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _unitOfWork.Students.GetAllAsync();
            var result = students.Select(s => new ReadStudentDto
            {
                Id = s.Id,
                FullName = s.FullName,
                Email = s.Email,
                Phone = s.Phone,
                BirthDate = s.BirthDate,
                Gender = s.Gender
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);

            if (student == null)
                return NotFound("Student not found.");

            var result = new ReadStudentDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                Phone = student.Phone,
                BirthDate = student.BirthDate,
                Gender = student.Gender
            };

            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,StudentDto dto)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);

            if (student == null)
                return NotFound("Student not found.");

            student.FullName = dto.FullName;
            student.Email = dto.Email;
            student.Phone = dto.Phone;
            student.BirthDate = dto.BirthDate;
            student.Gender = dto.Gender;

            _unitOfWork.Students.Update(student);

            await _unitOfWork.CompleteAsync();

            var result = new ReadStudentDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                Phone = student.Phone,
                BirthDate = student.BirthDate,
                Gender = student.Gender
            };

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);

            if (student == null)
                return NotFound("Student not found.");

            await _unitOfWork.Students.DeleteAsync(id);

            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name is required.");

            var students = await _unitOfWork.Students.SearchByNameAsync(name);

            var result = students.Select(s => new ReadStudentDto
            {
                Id = s.Id,
                FullName = s.FullName,
                Email = s.Email,
                Phone = s.Phone,
                BirthDate = s.BirthDate,
                Gender = s.Gender
            });

            return Ok(result);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter(string gender)
        {
            if (string.IsNullOrWhiteSpace(gender))
                return BadRequest("Gender is required.");

            var students = await _unitOfWork.Students.FilterByGenderAsync(gender);

            var result = students.Select(s => new ReadStudentDto
            {
                Id = s.Id,
                FullName = s.FullName,
                Email = s.Email,
                Phone = s.Phone,
                BirthDate = s.BirthDate,
                Gender = s.Gender
            });

            return Ok(result);
        }

    }
}
