using MusicCatalog.Domain.Exceptions;
using MusicCatalog.Domain.ValueObjects;

namespace MusicCatalog.Domain.Entities
{
    public class Album
    {
        public int Id { get; private set; }
        public AlbumName Name { get; private set; }
        public int ArtistId { get; private set; }
        public Artist Artist {get;private set;}
        private readonly List<Track> _tracks = new List<Track>();
        public IReadOnlyCollection<Track> Tracks => _tracks;

        public Album(AlbumName name, Artist artist)
        {
            ChangeName(name);
            SetArtist(artist);
        }

        private Album(){}

        public void ChangeName(AlbumName name)
        {
            if (name is null)
                throw new DomainException("Name não pode ser nulo.");

            Name = name;
        }

        private void SetArtist(Artist artist)
        {
            if (artist is null)
                throw new DomainException("Artist inválido.");

            Artist= artist;
            ArtistId = artist.Id;
        }
    }
}