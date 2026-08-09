namespace Case4_School.Repos.Interface
{
    public interface IUnitOfWork
    {
        IStudentRepo Students { get; }

        ITeacherRepo Teachers { get; }

        ICourseRepo Courses { get; }

        IEnrollmentRepo Enrollments { get; }

        IReportRepo Reports { get; }


        Task<int> CompleteAsync();
    }
}
