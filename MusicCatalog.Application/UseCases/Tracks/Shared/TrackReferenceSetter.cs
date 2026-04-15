using MusicCatalog.Application.Exceptions;
using MusicCatalog.Application.Interfaces.Persistence;
using MusicCatalog.Domain.Entities;

namespace MusicCatalog.Application.UseCases.Tracks.Shared
{
    public static class TrackReferenceSetter
    {
        public static async Task SetAlbumIdIfExistsAsync(IUnitOfWork unitOfWork, int albumId, Track track, CancellationToken cancellationToken, bool validateAlbumToArtist = false)
        {
            var albumEntity = await unitOfWork.AlbumRepository.GetAsync(x => x.Id == albumId, cancellationToken);

            if(albumEntity is null)
                throw new NotFoundException("Album not found");
            
            if(validateAlbumToArtist)
            {
                bool belongsToArtist = await AlbumBelongsToArtist(unitOfWork, track.ArtistId, albumEntity.ArtistId, cancellationToken);

                if(!belongsToArtist)
                    throw new BusinessRuleException("AlbumId not bellongs to Artist");
            }

            track.ChangeAlbumId(albumId);
        }

        public static async Task SetGenreIfExistsAsync(IUnitOfWork unitOfWork, int genreId, Domain.Entities.Track track, CancellationToken cancellationToken)
        {
            var genreExists = await unitOfWork.GenreRepository.ExistsAsync(x => x.Id == genreId, cancellationToken);

            if(!genreExists)
                throw new NotFoundException("Genre not found");

            track.ChangeGenreId(genreId);
        }

        public static async Task<bool> AlbumBelongsToArtist (IUnitOfWork unitOfWork, int trackArtistId, int albumArtistId, CancellationToken cancellationToken)
        {
            if(trackArtistId == albumArtistId)
                return true;

            return false; 
        }
       
    }
}