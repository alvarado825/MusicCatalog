using MusicCatalog.Domain.Entities;
using MusicCatalog.Domain.ValueObjects;

namespace MusicCatalog.Application.UseCases.Albums.Command.CreateAlbum
{
    public static class CreateAlbumMappers
    {
        public static Artist RequestToArtistEntityMapper(CreateArtistRequest request)
        {         
            return new Artist
            (
                name: new ArtistName(request.Name), 
                biography : request.Biography
            );            
        }

        public static Album RequestToAlbumEntityMapper(CreateAlbumRequest request, Artist artist)
        {         
            return new Album
            (
                name : new AlbumName(request.Title),
                artist : artist
            );            
        }

        public static CreateAlbumResponse ToResponseMapper(Album albumEntity)
        {
            return new CreateAlbumResponse
            {
                AlbumId = albumEntity.Id,
                AlbumName = albumEntity.Name.Value,
                Artist = new ArtistDto
                {
                    ArtistId = albumEntity.Artist.Id,
                    ArtistName = albumEntity.Artist.Name.Value
                }
               
            };
        }
    }
}