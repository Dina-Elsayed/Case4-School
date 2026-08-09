using Case4_School.Models;

namespace Case4_School.Repos.Interface
{
    public interface ITeacherRepo : IGenericRepo<Teacher>
    {
        Task<bool> HasCoursesAsync(int teacherId);
    }
}
