using MusicCatalog.Application.Interfaces.Persistence;
using MusicCatalog.Domain.Entities;
using MusicCatalog.Domain.Enums;

namespace MusicCatalog.Infrastructure.Persistence.Repositories
{
    public class TrackRepository : Repository<Track>, ITrackRepository
    {
        public TrackRepository(MusicCatalogDbContext context) : base(context)
        {
            
        }

        public IQueryable<Track> QueryPublishedAsync()
        {
            var tracks = Query().Where(x => x.TrackStatus == TrackStatusEnum.Published && x.IsActive == true);

            return tracks;
        }

    }
}