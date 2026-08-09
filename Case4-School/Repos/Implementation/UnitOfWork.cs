using Case4_School.AppDbContext;
using Case4_School.Repos.Interface;

namespace Case4_School.Repos.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly SchoolDbContext _context;

        public IStudentRepo Students { get; }

        public ITeacherRepo Teachers { get; }

        public ICourseRepo Courses { get; }

        public IEnrollmentRepo Enrollments { get; }

        public IReportRepo Reports { get; }



        public UnitOfWork(SchoolDbContext context, IStudentRepo studentRepo, ITeacherRepo teacherRepo, ICourseRepo courseRepo, IEnrollmentRepo enrollmentRepo, IReportRepo reports)
        {
            _context = context;
            Students = studentRepo;
            Teachers = teacherRepo;
            Courses = courseRepo;
            Enrollments = enrollmentRepo;
            Reports = reports;
        }



        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
