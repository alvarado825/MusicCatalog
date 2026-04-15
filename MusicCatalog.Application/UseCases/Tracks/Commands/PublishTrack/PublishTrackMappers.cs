using MusicCatalog.Domain.Entities;

namespace MusicCatalog.Application.UseCases.Tracks.Commands.PublishTrack
{
    public class PublishTrackMappers
    {
        public static PublishTrackResponse EntityToResponseMapper(Track entity)
        {
            return new PublishTrackResponse()
            {
                Id = entity.Id,
                Name = entity.Name.Value,
                GenreId = entity.GenreId,
                AlbumId = entity.AlbumId,
                ArtistId = entity.ArtistId,
                Status = entity.TrackStatus.ToString()
            };
        }   
    }
}