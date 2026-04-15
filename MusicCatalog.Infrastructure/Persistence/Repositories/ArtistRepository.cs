using MusicCatalog.Application.Interfaces.Persistence;
using MusicCatalog.Domain.Entities;

namespace MusicCatalog.Infrastructure.Persistence.Repositories
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        public ArtistRepository(MusicCatalogDbContext context) : base(context)
        {
            
        }
    }
}