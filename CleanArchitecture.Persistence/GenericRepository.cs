using CleanArchitecture.Application.Interfaces;

namespace CleanArchitecture.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly Context context;

        public GenericRepository(Context context)
        {
            this.context = context;
        }

        public async Task<T> Add(T entity) => (await context.AddAsync(entity)).Entity;

        public void Delete(T entity) => context.Remove(entity);

        public async Task<T> Get(int id)
        {
            var entity = await context.FindAsync<T>(id);
            if (entity is not null) context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            return entity;
        }

        public async Task Save() => await context.SaveChangesAsync();

        public void Update(T entity) => context.Update(entity);
    }
}
