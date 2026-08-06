using Case4_School.AppDbContext;
using Case4_School.Models;
using Case4_School.Repos.Interface;
using Case5_ClubSystem.Repos.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Case4_School.Repos.Implementation
{
    public class StudentRepo : GenericRepo<Student>, IStudentRepo
    {
        private readonly SchoolDbContext _context;

        public StudentRepo(SchoolDbContext context) : base(context)
        {
        }


        public async Task<IEnumerable<Student>> SearchByNameAsync(string name)
        {
            return await _context.Students
                .Include(s => s.StudentProfile)
                .Where(s => s.FullName.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> FilterByGenderAsync(string gender)
        {
            return await _context.Students
                .Include(s => s.StudentProfile)
                .Where(s => s.Gender.ToLower() == gender.ToLower())
                .ToListAsync();
        }




        public async Task<IEnumerable<Student>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Students
                .Include(s => s.StudentProfile)
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

    }
}
