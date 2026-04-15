namespace MusicCatalog.Application.UseCases.Tracks.Commands.PublishTrack
{
    public class PublishTrackResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int? GenreId { get; init; }
        public int? AlbumId { get; init; }
        public int ArtistId { get; init; }
        public string Status { get; init; }
    }
}