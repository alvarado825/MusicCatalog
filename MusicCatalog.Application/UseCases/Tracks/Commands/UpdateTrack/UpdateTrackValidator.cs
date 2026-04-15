using FluentValidation;
using MusicCatalog.Domain.Entities;
using MusicCatalog.Domain.Helpers;
using MusicCatalog.Domain.ValueObjects;

namespace MusicCatalog.Application.UseCases.Tracks.Commands.UpdateTrack
{
    public class UpdateTrackValidator : AbstractValidator<UpdateTrackRequest>
    {
        public UpdateTrackValidator()
        {
            RuleFor(x => x)
                .Must(HasAnyFieldToUpdate)
                .WithMessage("Provide at least one field for update.");
            
            RuleFor(x => x.Name)
                .Must(TrackName.IsValid)
                .When(x => x.Name is not null)
                .WithMessage($"Invalid Name, it's not to be empty and must be {TrackName.MaxNameLenght} characters lenght.");

            RuleFor(x => x.AlbumId)
                .GreaterThan(0)
                .When(x => x.AlbumId.HasValue)
                .WithMessage("AlbumId must be greater than 0");

            RuleFor(x => x.GenreId)
                .GreaterThan(0)
                .When(x => x.GenreId.HasValue)
                .WithMessage("GenreId must be greater than 0");
            
            RuleFor(x => x.Composer)
                .Must(IsAValidComposer)
                .When(x => x.Composer is not null)
                .WithMessage($"Invalid composer, this value no to be an empty and must be {Track.ComposerNameMaxLenght} characters lenght.");
            
            //Este campo é nullable, então se n for informado virá null, neste caso só validamos se houver valor
            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0)
                .When(x => x.UnitPrice.HasValue)
                .WithMessage("UnitPrice must be greater than or equal to 0.");
                  
        }

        private static bool HasAnyFieldToUpdate(UpdateTrackRequest request)
        {
            return request.Name is not null ||
                   request.AlbumId.HasValue ||
                   request.GenreId.HasValue ||
                   request.UnitPrice.HasValue ||
                   request.Composer is not null;
        }

        private static bool IsAValidComposer(string? composer)
        {
            if(string.IsNullOrWhiteSpace(composer))
                return false;
            
            string normalizedName = StringHelpers.Normalize(composer);

            if(normalizedName.Length > Track.ComposerNameMaxLenght)
                return false;
            
            return true;
        }
    }
}