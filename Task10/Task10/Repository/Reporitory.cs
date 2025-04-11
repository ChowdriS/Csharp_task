using Microsoft.EntityFrameworkCore;
using Task10.Data;
using Task10.Interface;

namespace Task10.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext context;
        private readonly DbSet<T> db;

        public Repository(AppDbContext context)
        {
            this.context = context;
            db = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await db.ToListAsync();

        public async Task<T?> GetByIdAsync(Guid id) => await db.FindAsync(id);

        public async Task AddAsync(T entity) => await db.AddAsync(entity);

        public void Update(T entity) => db.Update(entity);

        public void Delete(T entity) => db.Remove(entity);

        public async Task SaveAsync() => await context.SaveChangesAsync();
    }
}
