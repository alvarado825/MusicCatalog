using MusicCatalog.Domain.Entities;

namespace MusicCatalog.Application.UseCases.Tracks.Commands.UpdateTrack
{
    public class UpdateTrackMappers
    {
         public static UpdateTrackResponse EntityToResponseMapper(Track entity)
        {
            return new UpdateTrackResponse()
            {
                Id = entity.Id,
                TrackName = entity.Name.Value,
                ArtistId = entity.ArtistId,
                AlbumId = entity.AlbumId,
                GenreId = entity.GenreId,
                Composer = entity.Composer,
                Duration = entity.Duration,
                Bytes = entity.Bytes,
                UnitPrice = entity.UnitPrice,
                TrackStatus = entity.TrackStatus.ToString(), 
                IsActive = entity.IsActive     
            };
        }   
    }
}