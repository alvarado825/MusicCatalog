using FluentValidation;
using MusicCatalog.Application.Exceptions;
using MusicCatalog.Application.Interfaces.Persistence;
using MusicCatalog.Domain.Entities;

namespace MusicCatalog.Application.UseCases.Albums.Command.CreateAlbum
{
    public class CreateAlbumUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateAlbumRequest> _validator;

        public CreateAlbumUseCase(IUnitOfWork unitOfWork, IValidator<CreateAlbumRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<CreateAlbumResponse> ExecuteAsync(CreateAlbumRequest request, CancellationToken cancellationToken)
        {
            await ValidateRequestAsync(request, cancellationToken);

            Artist artistEntity;
    
            if (request.Artist is not null)
            {
                Artist NewArtistEntity = CreateAlbumMappers.RequestToArtistEntityMapper(request.Artist);
                
                await _unitOfWork.ArtistRepository.CreateAsync(NewArtistEntity, cancellationToken);

                artistEntity = NewArtistEntity;
            }
            else
            {
                artistEntity = await GetArtistOrThrowIfNotExistsAsync(request, cancellationToken);
            }


            var albumCreated = CreateAlbumMappers.RequestToAlbumEntityMapper(request, artistEntity);

            await _unitOfWork.AlbumRepository.CreateAsync(albumCreated, cancellationToken);


            await _unitOfWork.CommitAsync(cancellationToken);


            return CreateAlbumMappers.ToResponseMapper(albumCreated);
        }

        private async Task ValidateRequestAsync(CreateAlbumRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
           
        }

        private async Task<Artist> GetArtistOrThrowIfNotExistsAsync(CreateAlbumRequest request, CancellationToken cancellationToken)
        {
             Artist artistEntity = null;

            artistEntity = await _unitOfWork.ArtistRepository.GetAsync(x => x.Id == request.ArtistId, cancellationToken);

            if(artistEntity is null)
                throw new NotFoundException("A provided ArtistId was not found");
                                   
            return artistEntity;
        }
    }
}