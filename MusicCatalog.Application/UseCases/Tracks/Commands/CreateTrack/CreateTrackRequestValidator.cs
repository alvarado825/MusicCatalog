using FluentValidation;
using MusicCatalog.Domain.ValueObjects;
using System.Globalization;

namespace MusicCatalog.Application.UseCases.Tracks.Commands.CreateTrack
{
    public class CreateTrackRequestValidator : AbstractValidator<CreateTrackRequest>
    {
        public CreateTrackRequestValidator()
        {
            RuleFor(x => x.Name)
                .Must(TrackName.IsValid)
                .WithMessage($"Name is required and not be null or empty spaces, it must be at most {TrackName.MaxNameLenght} characters");
                

            RuleFor(x => x.Duration)
                .Must(IsAValidAndPositiveDuration)
                .WithMessage("Duration is required and must be in format 'mm:ss' and greater than 0 (e.g: 05:30).");

            RuleFor(x => x.ArtistId)
                .NotEmpty()
                .WithMessage("ArtistId is required, must be a integer greater than 0 and correspond to an existing Artist.");

            RuleFor(x => x.GenreId)
                .GreaterThan(0)
                .WithMessage("GenreId must be a integer, greater than 0 and correspond to an existing Genre.")
                .When(x => x.GenreId.HasValue);

            RuleFor(x => x.AlbumId)
                .GreaterThan(0)
                .WithMessage("AlbumId must be a integer, greater than 0 and correspond to an existing Album.")
                .When(x => x.GenreId.HasValue);

            RuleFor(x => x.Bytes)
                .NotEmpty()
                .WithMessage("Bytes is required and must be a integer and greater than 0.");

            //Este campo não é nullable, então se n for informado virá como 0 e nunca null, se no request vier null o proprio model bind do aspt net
            //retorna erro, neste caso n precisa de notempty
            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("UnitPrice must be greater or equal to 0.");           
        }

        private static bool IsAValidAndPositiveDuration(string duration)
        {
            if(string.IsNullOrWhiteSpace(duration))
                return false;

             var convertedDuration = CreateTrackParsers.ToDurationFormatParser(duration);

            return convertedDuration > TimeSpan.Zero;
        }     
    }
}