using FluentValidation;

namespace MusicCatalog.Application.Common.Pagination
{
    public class PaginationRequestParametersValidation<T> : AbstractValidator<T> where T : PaginationRequestParameters
    {
        public PaginationRequestParametersValidation()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("O valor de Page deve ser maior que 0");
            
            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 50)
                .WithMessage("O valor de PageSize deve ser maior que 0");
        }
    }
}