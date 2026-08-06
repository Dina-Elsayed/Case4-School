using Case4_School.Models;

namespace Case4_School.Repos.Interface
{
    public interface ICourseRepo : IGenericRepo<Course>
    {
        Task<IEnumerable<Course>> SearchByTitleAsync(string title);

        Task<IEnumerable<Course>> FilterCoursesAsync(int? teacherId, int? creditHours);

        Task<bool> HasStudentsAsync(int courseId);

        Task<IEnumerable<CourseStatisticsDto>> GetCourseStatisticsAsync();

        Task<IEnumerable<AverageGradeDto>> GetAverageGradesAsync();

        Task<IEnumerable<TopStudentDto>> GetTopStudentsAsync();
    }
}
