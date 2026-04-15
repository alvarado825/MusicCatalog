using MusicCatalog.Domain.Exceptions;
using MusicCatalog.Domain.Helpers;

namespace MusicCatalog.Domain.ValueObjects
{
    public class AlbumName
    {
        public const int MaxNameLenght = 50;

        public string Value {get;}

        public AlbumName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new InvalidNameException();
            
            string normalizedName = StringHelpers.Normalize(name);

            if(normalizedName.Length > MaxNameLenght)
            {
                throw new InvalidNameException($"Name must be {MaxNameLenght} characters or less.");
            }

            Value = normalizedName;
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