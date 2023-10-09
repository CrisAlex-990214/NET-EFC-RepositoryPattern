namespace CleanArchitecture.Application.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> Add(T entity);
        Task<T> Get(int id);
        void Update(T entity);
        void Delete(T entity);
        Task Save();
    }
}
