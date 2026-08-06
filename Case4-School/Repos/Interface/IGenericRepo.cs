namespace Case4_School.Repos.Interface
{
    public interface IGenericRepo<T> where T : class
    {


        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
       Task DeleteAsync(int id);
    }
}
