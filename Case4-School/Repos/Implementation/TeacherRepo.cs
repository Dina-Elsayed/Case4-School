using Case4_School.AppDbContext;
using Case4_School.Models;
using Case4_School.Repos.Interface;
using Case5_ClubSystem.Repos.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Case4_School.Repos.Implementation
{
    public class TeacherRepo : GenericRepo<Teacher>, ITeacherRepo
    {
       
        private readonly SchoolDbContext _context;

        public TeacherRepo(SchoolDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<bool> HasCoursesAsync(int teacherId)
        {

            return await _context.Courses.AnyAsync(c => c.TeacherId == teacherId);

        }

     
    }
}
