namespace MusicCatalog.Application.UseCases.Tracks.Commands.CreateTrack
{
    public class CreateTrackResponse
    {
        public int Id { get; init; }
        public string TrackName { get; init; }
        public int ArtistId {get; init;}
        public int? AlbumId { get; init; }
        public int? GenreId { get; init; }
        public string? Composer { get; init; }
        public TimeSpan Duration { get; init; }
        public int Bytes { get; init; }
        public Decimal UnitPrice { get; init; }
        public string TrackStatus {get; init;}
        public bool IsActive {get; init;}
    }
}