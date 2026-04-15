namespace MusicCatalog.Application.UseCases.Albums.Command.CreateAlbum
{
    public class CreateAlbumRequest
    {
        public string Title {get;init;}

        public int? ArtistId {get;init;}

        public CreateArtistRequest? Artist {get; init;}
    }

    public class CreateArtistRequest
    {
        public string Name {get;init;}

        public string? Biography {get;init;}
    }
}