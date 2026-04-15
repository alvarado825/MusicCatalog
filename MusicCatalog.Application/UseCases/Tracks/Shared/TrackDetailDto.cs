using MusicCatalog.Domain.Enums;

namespace MusicCatalog.Application.UseCases.Tracks.Shared
{
    public class TrackDetailDto
    {
        public int Id { get; private set; }
        public string TrackName { get; private set; }
        public int? AlbumId { get; private set; }
        public int? GenreId { get; private set; }
        public int ArtistId {get; private set;}
        public string? Composer { get; private set; }
        public TimeSpan Duration { get; private set; }
        public int Bytes { get; private set; }
        public Decimal UnitPrice { get; private set; }
        public TrackStatusEnum TrackStatus {get; private set;}
        public bool IsActive {get; private set;}
        
    }
}