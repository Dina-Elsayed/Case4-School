using Case4_School.Repos.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Case4_School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;

        public ReportsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet("course-statistics")]
        public async Task<IActionResult> CourseStatistics()
        {
            var result =
                await _unitOfWork.Reports.GetCourseStatisticsAsync();

            return Ok(result);
        }

        [HttpGet("average-grades")]
        public async Task<IActionResult> AverageGrades()
        {
            var result =
                await _unitOfWork.Reports.GetAverageGradesAsync();

            return Ok(result);
        }

        [HttpGet("top-students")]
        public async Task<IActionResult> TopStudents()
        {
            var result =
                await _unitOfWork.Reports.GetTopStudentsAsync();

            return Ok(result);
        }

    }
}
