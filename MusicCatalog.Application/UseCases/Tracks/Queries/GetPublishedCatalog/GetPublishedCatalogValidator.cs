using FluentValidation;
using MusicCatalog.Application.Common.Pagination;

namespace MusicCatalog.Application.UseCases.Tracks.Queries.GetPublishedCatalog
{
    public class GetPublishedCatalogValidator : PaginationRequestParametersValidation<GetPublishedCatalogRequest>
    {
        public GetPublishedCatalogValidator()
        {           
            RuleFor(x => x.ArtistId)
                .GreaterThan(0)
                .WithMessage("ArtistId must be greather than 0")
                .When(x => x.ArtistId.HasValue);
            
            RuleFor(x => x.GenreId)
                .GreaterThan(0)
                .WithMessage("GenreId must be greather than 0")
                .When(x => x.GenreId.HasValue);
        }
        
    }
}