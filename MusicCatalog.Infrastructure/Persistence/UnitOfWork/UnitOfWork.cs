using MusicCatalog.Application.Interfaces.Persistence;

namespace MusicCatalog.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MusicCatalogDbContext _context;

        public UnitOfWork(
        MusicCatalogDbContext context,
        IAlbumRepository albumRepository,
        IArtistRepository artistRepository,
        IGenreRepository genreRepository,
        ITrackRepository trackRepository)
        {
        _context = context;

        AlbumRepository = albumRepository;
        ArtistRepository = artistRepository;
        GenreRepository = genreRepository;
        TrackRepository = trackRepository;
        }


        public IAlbumRepository AlbumRepository { get;}

        public IArtistRepository ArtistRepository { get;}

        public IGenreRepository GenreRepository { get;}

        public ITrackRepository TrackRepository { get;}


        public Task CommitAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}