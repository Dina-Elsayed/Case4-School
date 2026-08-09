using Case4_School.AppDbContext;
using Case4_School.DTOs;
using Case4_School.Repos.Interface;
using Microsoft.EntityFrameworkCore;

namespace Case4_School.Repos
{
    public class ReportRepo : IReportRepo
    {

        private readonly SchoolDbContext _context;

        public ReportRepo(SchoolDbContext context)
        {
            _context = context;
        }




        public async Task<IEnumerable<CourseStatisticsDto>> GetCourseStatisticsAsync()
        {
            return await _context.Courses
                .Select(c => new CourseStatisticsDto
                {
                    CourseTitle = c.Title,

                    TeacherName = c.Teacher!.FullName,

                    StudentCount = c.Enrollments.Count()
                })
                .ToListAsync();
        }



        public async Task<IEnumerable<AverageGradeDto>> GetAverageGradesAsync()
        {
            return await _context.Courses
                .Select(c => new AverageGradeDto
                {
                    CourseTitle = c.Title,

                    AverageGrade = c.Enrollments
                        .Where(e => e.Grade.HasValue)
                        .Average(e => e.Grade) ?? 0
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<TopStudentDto>> GetTopStudentsAsync()
        {
            return await _context.Students
                .Select(s => new TopStudentDto
                {
                    StudentName = s.FullName,

                    AverageGrade = s.Enrollments
                        .Where(e => e.Grade.HasValue)
                        .Average(e => e.Grade) ?? 0
                })
                .OrderByDescending(s => s.AverageGrade)
                .Take(10)
                .ToListAsync();
        }

    }
}
