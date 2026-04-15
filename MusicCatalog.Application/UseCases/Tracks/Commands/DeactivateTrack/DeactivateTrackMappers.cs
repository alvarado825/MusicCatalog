using MusicCatalog.Domain.Entities;

namespace MusicCatalog.Application.UseCases.Tracks.Commands.DeactivateTrack
{
    public class DeactivateTrackMappers
    {
        public static DeactivateTrackResponse EntityToResponseMapper(Track entity)
        {
            return new DeactivateTrackResponse()
            {
                Id = entity.Id,
                Name = entity.Name.Value,
                AlbumId = entity.AlbumId,
                ArtistId = entity.ArtistId,
                Status = entity.TrackStatus.ToString(),
                IsActive = entity.IsActive
            };
        }   
    }
}