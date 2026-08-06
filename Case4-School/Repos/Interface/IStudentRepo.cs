using Case4_School.Models;

namespace Case4_School.Repos.Interface
{
    public interface IStudentRepo : IGenericRepo<Student>
    {

        Task<IEnumerable<Student>> SearchByNameAsync(string name);

        Task<IEnumerable<Student>> FilterByGenderAsync(string gender);

        Task<IEnumerable<Student>> GetPagedAsync(
            int pageNumber,
            int pageSize);
    }
}
