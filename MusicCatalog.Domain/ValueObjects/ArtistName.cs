using MusicCatalog.Domain.Exceptions;
using MusicCatalog.Domain.Helpers;

namespace MusicCatalog.Domain.ValueObjects
{
    public class ArtistName
    {
        public const int MaxNameLenght = 100;

        public string Value {get;}

        public ArtistName(string name)
        {
            if(!IsValid(name))
                throw new InvalidNameException($"Name is required and not be null or empty spaces, it must be at most {MaxNameLenght} characters");

            Value = StringHelpers.Normalize(name);
        }

        public static bool IsValid(string? name)
        {
            if(string.IsNullOrWhiteSpace(name))
                return false;

            string normalizedName = StringHelpers.Normalize(name);

            if(normalizedName.Length > MaxNameLenght)
            {
                return false;
            }

            return true;
        }
    }
}