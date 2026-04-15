using System.Globalization;

namespace MusicCatalog.Application.UseCases.Tracks.Commands.CreateTrack
{
    public static class CreateTrackParsers
    {
        public static TimeSpan ToDurationFormatParser(string duration)
        {        
            var converted = TimeSpan.TryParseExact(duration, @"mm\:ss", CultureInfo.InvariantCulture, out var convertedDuration);

            if (!converted)
                return TimeSpan.Zero;

            return convertedDuration;
        }
    }
}