namespace MusicCatalog.Application.UseCases.Tracks.Commands.CreateTrack
{
    public class CreateTrackRequest
    {
        public string Name { get; init; }
        public int ArtistId {get;init;}
        public int? GenreId { get; init; }
        public int? AlbumId { get; init; }
        public string? Composer { get; init; }
        public string Duration { get; init; }
        public int Bytes { get; init; }
        public decimal UnitPrice { get; init; }
    }
}