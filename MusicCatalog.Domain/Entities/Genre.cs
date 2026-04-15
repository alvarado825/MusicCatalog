using MusicCatalog.Domain.Exceptions;
using MusicCatalog.Domain.ValueObjects;

namespace MusicCatalog.Domain.Entities
{
    public class Genre
    {
        public int Id { get; private set;}
        public GenreName Name { get;private set;}

        public Genre(GenreName name)
        {
            ChangeName(name);
        }

        private Genre(){}

        public void ChangeName(GenreName name)
        {
            if (name is null)
                throw new DomainException("Name is required.");

            Name = name;
        }
    }
}