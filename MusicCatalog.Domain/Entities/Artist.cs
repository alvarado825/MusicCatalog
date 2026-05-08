using MusicCatalog.Domain.Exceptions;
using MusicCatalog.Domain.Helpers;
using MusicCatalog.Domain.ValueObjects;

namespace MusicCatalog.Domain.Entities
{
    public class Artist
    {
        public const int MaxLenghtBiography = 1000;

        public int Id { get; private set; }
        public ArtistName Name { get; private set; }
        public string? Biography {get; private set;}

        private readonly List<Album> _albums = new List<Album>();
        public IReadOnlyCollection<Album> Albums => _albums;

        public Artist(ArtistName name, string? biography)
        {
            ChangeName(name);
            ChangeBiography(biography);
        }

        private Artist(){}

        public void ChangeName(ArtistName name)
        {
            if (name is null)
                throw new DomainException("Name is required.");

            Name = name;
        }

        public void ChangeBiography(string? biography)
        {
            if (string.IsNullOrWhiteSpace(biography))
            {
                Biography = null;
            }
            else
            {
                var normalizedBiography = StringHelpers.Normalize(biography);

                if (normalizedBiography.Length > MaxLenghtBiography)
                    throw new DomainException($"Biography must be {MaxLenghtBiography} characters lenght.");

                Biography = normalizedBiography;
            }
        }

    }
}