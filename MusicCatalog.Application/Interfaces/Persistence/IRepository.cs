using System.Linq.Expressions;

namespace MusicCatalog.Application.Interfaces.Persistence
{
    public interface IRepository <T>
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancelationToken);
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancelationToken);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancelationToken);
        IQueryable<T> Query();
        Task CreateAsync (T entity, CancellationToken cancelationToken);
        void Update (T entity);
        void Delete (T entity);
    }
}