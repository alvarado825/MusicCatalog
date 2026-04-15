namespace MusicCatalog.Application.UseCases.Tracks.Commands.UpdateTrack
{
    public class UpdateTrackRequest
    {
        public string? Name { get; init; }
        public int? AlbumId { get; init; }
        public int? GenreId { get; init; }
        public string? Composer { get; init; }     
        public decimal? UnitPrice { get; init; }   
    }
}