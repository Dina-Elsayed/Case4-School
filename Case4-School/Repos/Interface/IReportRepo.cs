using Case4_School.DTOs;

namespace Case4_School.Repos.Interface
{
    public interface IReportRepo 
    {

        Task<IEnumerable<CourseStatisticsDto>> GetCourseStatisticsAsync();

        Task<IEnumerable<AverageGradeDto>> GetAverageGradesAsync();

        Task<IEnumerable<TopStudentDto>> GetTopStudentsAsync();
    }
}
