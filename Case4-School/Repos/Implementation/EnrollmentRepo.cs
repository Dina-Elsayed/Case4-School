using Case4_School.AppDbContext;
using Case4_School.Models;
using Case4_School.Repos.Interface;
using Case5_ClubSystem.Repos.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Case4_School.Repos.Implementation
{
    public class EnrollmentRepo : GenericRepo<Enrollment>, IEnrollmentRepo
    {
        private readonly SchoolDbContext _context;
        public EnrollmentRepo(SchoolDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Enrollment>> GetByCourseAsync(int courseId)
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Where(e => e.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Enrollment>> GetByStudentAsync(int studentId)
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Where(e => e.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<bool> IsStudentEnrolledAsync(int studentId, int courseId)
        {
            return await _context.Enrollments
                .AnyAsync(e =>
                    e.StudentId == studentId &&
                    e.CourseId == courseId);
        }
    }
}
