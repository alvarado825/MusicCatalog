namespace MusicCatalog.Application.UseCases.Tracks.Commands.DeactivateTrack
{
    public class DeactivateTrackResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int? AlbumId { get; init; }
        public int ArtistId { get; init; }
        public string? Composer { get; init; }
        public string Status { get; init; }    
        public bool IsActive {get;set;}
    }
}