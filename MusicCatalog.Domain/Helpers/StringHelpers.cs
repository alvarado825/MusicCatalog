using System.Text.RegularExpressions;

namespace MusicCatalog.Domain.Helpers
{
    public class StringHelpers
    {
        public static string Normalize(string name)
        {
            var trimmed = name.Trim();

            var singleSpaced = Regex.Replace(trimmed, @"\s+", " ");
            
            return singleSpaced;
        }
    }
}