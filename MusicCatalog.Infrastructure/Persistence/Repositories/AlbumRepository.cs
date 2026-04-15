using MusicCatalog.Application.Interfaces.Persistence;
using MusicCatalog.Domain.Entities;

namespace MusicCatalog.Infrastructure.Persistence.Repositories
{
    public class AlbumRepository : Repository<Album>, IAlbumRepository
    {
        public AlbumRepository(MusicCatalogDbContext context) : base(context)
        {
        }
    }
}