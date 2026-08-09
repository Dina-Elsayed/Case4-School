using Case4_School.AppDbContext;
using Case4_School.DTOs;
using Case4_School.Models;
using Case4_School.Repos.Interface;
using Case5_ClubSystem.Repos.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Case4_School.Repos.Implementation
{
    public class CourseRepo : GenericRepo<Course>, ICourseRepo
    {

        private readonly SchoolDbContext _context;

        public CourseRepo(SchoolDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Course>> FilterCoursesAsync(int? teacherId, int? creditHours)
        {
            IQueryable<Course> query = _context.Courses.Include(c => c.Teacher);
            if (teacherId.HasValue)
                query = query.Where(c => c.TeacherId == teacherId);

            if (creditHours.HasValue)
                query = query.Where(c => c.CreditHours == creditHours);

            return await query.ToListAsync();

        }

       
        public async Task<bool> HasStudentsAsync(int courseId)
        {
            return await _context.Enrollments.AnyAsync(e => e.CourseId == courseId);
        }

        public async Task<IEnumerable<Course>> SearchByTitleAsync(string title)
        {
            return await _context.Courses.Include(c => c.Teacher)
                .Where(c => c.Title.Contains(title))
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllWithTeacherAsync()
        {
            return await _context.Courses
                .Include(c => c.Teacher)
                .ToListAsync();
        }

        public async Task<Course?> GetCourseWithTeacherAsync(int courseid)
        {
            return await _context.Courses
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(c => c.Id == courseid);
        }
    }
}
