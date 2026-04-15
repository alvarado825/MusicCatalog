namespace MusicCatalog.Application.UseCases.Albums.Command.CreateAlbum
{
    public class CreateAlbumResponse
    {
        public int AlbumId {get;init;}

        public string AlbumName {get;init;}

        public ArtistDto Artist{get;set;}

    }

    public class ArtistDto
    {
        public int ArtistId {get;init;}

        public string ArtistName {get;init;}
    }
}