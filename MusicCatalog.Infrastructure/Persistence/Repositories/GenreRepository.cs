using MusicCatalog.Application.Interfaces.Persistence;
using MusicCatalog.Domain.Entities;

namespace MusicCatalog.Infrastructure.Persistence.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(MusicCatalogDbContext context) : base(context)
        {
            
        }
    }
}