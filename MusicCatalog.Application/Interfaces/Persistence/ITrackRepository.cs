using MusicCatalog.Domain.Entities;

namespace MusicCatalog.Application.Interfaces.Persistence
{
    public interface ITrackRepository : IRepository<Track>
    {
        public IQueryable<Track> QueryPublishedAsync() ;
    }
}