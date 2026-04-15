using MusicCatalog.Domain.Entities;
using MusicCatalog.Domain.Enums;
using MusicCatalog.Domain.ValueObjects;

namespace MusicCatalog.Application.UseCases.Tracks.Commands.CreateTrack
{
    public static class CreateTrackMappers
    {
        public static Track RequestToEntityMapper(CreateTrackRequest request)
        {
            return new Track
            (
                name : new TrackName(request.Name),
                albumId : request.AlbumId,
                genreId : request.GenreId,
                artistId : request.ArtistId,
                composer : request.Composer,
                duration : CreateTrackParsers.ToDurationFormatParser(request.Duration),
                bytes : request.Bytes,
                unitPrice : request.UnitPrice,
                trackStatus : TrackStatusEnum.Draft,
                isActive : true
            );
        }

        public static CreateTrackResponse EntityToResponseMapper(Track entity)
        {
            return new CreateTrackResponse()
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