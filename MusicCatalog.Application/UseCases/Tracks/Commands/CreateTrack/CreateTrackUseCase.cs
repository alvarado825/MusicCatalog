using FluentValidation;
using MusicCatalog.Application.Exceptions;
using MusicCatalog.Application.Interfaces.Persistence;
using MusicCatalog.Application.UseCases.Tracks.Shared;
using MusicCatalog.Domain.Entities;
using MusicCatalog.Domain.Enums;
using MusicCatalog.Domain.ValueObjects;

namespace MusicCatalog.Application.UseCases.Tracks.Commands.CreateTrack
{
    public class CreateTrackUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateTrackRequest> _validator;


        public CreateTrackUseCase(IUnitOfWork unitOfWork, IValidator<CreateTrackRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<CreateTrackResponse> ExecuteAsync(CreateTrackRequest request, CancellationToken cancellationToken)
        {
            await ValidateRequestAsync(request, cancellationToken);

            await ValidateIfArtistExistsAsync(request, cancellationToken);

            await EnsureTrackUniqueForArtistAsync(request, cancellationToken);


            Track trackEntity = await CreateTrackEntityAsync(request, cancellationToken);

         
            await _unitOfWork.TrackRepository.CreateAsync(trackEntity, cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);


            return CreateTrackMappers.EntityToResponseMapper(trackEntity);
        }       

        private async Task ValidateRequestAsync (CreateTrackRequest request, CancellationToken cancellationToken)
        {           
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }    
        }

        private async Task ValidateIfArtistExistsAsync(CreateTrackRequest request, CancellationToken cancellationToken)
        {
            bool artistExists = await _unitOfWork.ArtistRepository.ExistsAsync(x => x.Id == request.ArtistId, cancellationToken);

            if(!artistExists)
                throw new NotFoundException("Artist not found.");
        }

        private async Task EnsureTrackUniqueForArtistAsync(CreateTrackRequest request, CancellationToken cancellationToken)
        {
            string trackName = new TrackName(request.Name).Value;

            var trackAlreadyExistForThisArtist = await _unitOfWork.TrackRepository.ExistsAsync(x => x.ArtistId == request.ArtistId &&
                                                                                               x.Name == new TrackName(request.Name),
                                                                                               cancellationToken);
            if(trackAlreadyExistForThisArtist)
                throw new AlreadyExistsException(ErrorCode.TrackAlreadyExistToThisArtist, "This track already exists for this artist. Check if this track is Inactive or not published");
        }

        private async Task<Track> CreateTrackEntityAsync(CreateTrackRequest request, CancellationToken cancellationToken)
        {
            Track trackEntity = CreateTrackMappers.RequestToEntityMapper(request);

            if(request.AlbumId is not null)
                await TrackReferenceSetter.SetAlbumIdIfExistsAsync(_unitOfWork, request.AlbumId.Value,trackEntity, cancellationToken, true);

            if(request.GenreId is not null)
                await TrackReferenceSetter.SetGenreIfExistsAsync(_unitOfWork, request.GenreId.Value,trackEntity, cancellationToken);
            

            return trackEntity;
        }
       
    }
}