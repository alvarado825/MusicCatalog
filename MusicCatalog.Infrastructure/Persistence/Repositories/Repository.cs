using System.Linq.Expressions;
using MusicCatalog.Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MusicCatalog.Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly MusicCatalogDbContext _context;

        public Repository(MusicCatalogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct)
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync(ct);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate, ct);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
        {
            return await _context.Set<T>().AnyAsync(predicate, ct);
        }
        
        public IQueryable<T> Query()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task CreateAsync(T entity, CancellationToken ct)
        {
            await _context.Set<T>().AddAsync(entity, ct);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}