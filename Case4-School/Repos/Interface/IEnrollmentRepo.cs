using Case4_School.Models;

namespace Case4_School.Repos.Interface
{
    public interface IEnrollmentRepo : IGenericRepo<Enrollment>
    {
        Task<IEnumerable<Enrollment>> GetByStudentAsync(int studentId);

        Task<IEnumerable<Enrollment>> GetByCourseAsync(int courseId);

        Task<bool> IsStudentEnrolledAsync(int studentId,int courseId);
    }
}
