using FluentValidation;
using MusicCatalog.Domain.ValueObjects;

namespace MusicCatalog.Application.UseCases.Albums.Command.CreateAlbum
{
    public class CreateAlbumValidator : AbstractValidator<CreateAlbumRequest>
    {
        public CreateAlbumValidator()
        {
            RuleFor(x => x.Title)
                .Must(AlbumName.IsValid)
                .WithMessage($"Title is required and not be null or empty spaces, it must be at most {AlbumName.MaxNameLenght} characters.");

            RuleFor(x => x)
                .Must(x => !(x.ArtistId.HasValue && x.Artist is not null))
                .WithMessage("Provide only ArtistId or Artist, both not allowed.");
            
            RuleFor(x => x)
                .Must(x => x.ArtistId.HasValue || x.Artist is not null)
                .WithMessage("Provide ArtistId Or Artist.");

            RuleFor(x => x.ArtistId)
                .GreaterThan(0)
                .When(x => x.ArtistId.HasValue)
                .WithMessage("ArtistId must be greater than 0");        
            
             When(x => x.Artist is not null, () =>
                {
                    RuleFor(x => x.Artist.Name)
                        .Must(ArtistName.IsValid)
                        .WithMessage($"Artist.Name is required and not be null or empty spaces, it must be at most {ArtistName.MaxNameLenght} characters.");
                });          
        }     
    }
}