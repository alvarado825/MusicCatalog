namespace MusicCatalog.Application.UseCases.Tracks.Queries.GetTrackById
{
    public class GetTrackByIdResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int? GenreId { get; init; }
        public string? GenreName { get; init; }
        public int? AlbumId { get; init; }
        public string? AlbumName { get; init; }
        public int ArtistId { get; init; }
        public string ArtistName { get; init; }
        public TimeSpan Duration {get; init;}
        public int Bytes {get; init;}
        public Decimal Price{get ;init;}
        public bool IsActive {get;init;}
        public string Status { get; init; }
    }
}