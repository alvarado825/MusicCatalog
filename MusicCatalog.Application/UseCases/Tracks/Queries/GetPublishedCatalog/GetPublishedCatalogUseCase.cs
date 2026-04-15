using FluentValidation;
using MusicCatalog.Application.Exceptions;
using MusicCatalog.Application.Interfaces.Persistence;
using MusicCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MusicCatalog.Application.UseCases.Tracks.Queries.GetPublishedCatalog
{
    public class GetPublishedCatalogUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GetPublishedCatalogRequest> _validator;

        public GetPublishedCatalogUseCase(IUnitOfWork unitOfWork, IValidator<GetPublishedCatalogRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<GetPublishedCatalogResponse> ExecuteAsync(GetPublishedCatalogRequest request, CancellationToken cancellationToken)
        {
            await ValidateRequestAsync( request, cancellationToken);

            await EnsureFilteredEntitiesExistsAsync(request, cancellationToken);

            var tracksQuery = BuildFilteredTracksQuery(request);

            var totalTrackItems =  await tracksQuery.CountAsync(cancellationToken);

            var skip = (request.Page - 1) * request.PageSize;               

            var items = await 
                (
                    from t in tracksQuery

                    join ar in _unitOfWork.ArtistRepository.Query() 
                        on t.ArtistId equals ar.Id


                    join al in _unitOfWork.AlbumRepository.Query() 
                        on t.AlbumId equals al.Id into albumGroup

                    from al in albumGroup.DefaultIfEmpty()


                    join ge in _unitOfWork.GenreRepository.Query()
                        on t.GenreId equals ge.Id into genreGroup
                    
                    from ge in genreGroup.DefaultIfEmpty()

                    select new PublicCatalogItemDto
                    {
                        TrackId = t.Id,
                        TrackName = t.Name.Value,
                        ArtistId = t.ArtistId,
                        ArtistName = ar.Name.Value,
                        AlbumId = t.AlbumId == null ? null : t.AlbumId,
                        AlbumName = al != null ? al.Name.Value : null,
                        GenreId = t.GenreId == null ? null : t.GenreId,
                        GenreName = ge != null ? ge.Name.Value : null,
                    }
                )            
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            // Calcula o total de páginas.
            var totalPages = (int)Math.Ceiling((double)totalTrackItems / request.PageSize);
            
            return GetPublishedCatalogMappers.ToResponseMapper(request, totalTrackItems, totalPages, items);          
        }

        private async Task ValidateRequestAsync(GetPublishedCatalogRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        } 

        private async Task EnsureFilteredEntitiesExistsAsync(GetPublishedCatalogRequest request, CancellationToken cancellationToken)
        {          
            if(request.ArtistId.HasValue)
            {
                bool artistExists = await _unitOfWork.ArtistRepository.ExistsAsync(x => x.Id == request.ArtistId.Value, cancellationToken);

                if(!artistExists)
                    throw new NotFoundException("O valor parametrizado em ArtistId não corresponde a nenhum artista existente.");
            }

            if(request.GenreId.HasValue)
            {
                bool genreExists = await _unitOfWork.GenreRepository.ExistsAsync(x => x.Id == request.GenreId.Value, cancellationToken);

                if(!genreExists)
                    throw new NotFoundException("O valor parametrizado em GenreId não corresponde a nenhum genre existente.");
            }      
        }      

        private IQueryable<Track> BuildFilteredTracksQuery(GetPublishedCatalogRequest request)
        {
            var tracksQuery = _unitOfWork.TrackRepository.QueryPublishedAsync();

            if(request.ArtistId.HasValue)
                tracksQuery = tracksQuery.Where(x => x.ArtistId == request.ArtistId);
            
            if(request.GenreId.HasValue)
                tracksQuery = tracksQuery.Where(x => x.GenreId == request.GenreId);


            return tracksQuery;            
        }
    }
}