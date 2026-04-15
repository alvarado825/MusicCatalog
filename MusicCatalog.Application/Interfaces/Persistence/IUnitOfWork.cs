namespace MusicCatalog.Application.Interfaces.Persistence
{
    public interface IUnitOfWork
    {
        IAlbumRepository AlbumRepository {get;}
        IArtistRepository ArtistRepository {get;}
        IGenreRepository GenreRepository {get;}
        ITrackRepository TrackRepository{get;}
        Task CommitAsync(CancellationToken cancellationToken);
    }
}