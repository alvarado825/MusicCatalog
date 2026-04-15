using FluentValidation;
using MusicCatalog.Application.Exceptions;
using MusicCatalog.Application.Interfaces.Persistence;
using MusicCatalog.Application.UseCases.Tracks.Shared;
using MusicCatalog.Domain.Entities;
using MusicCatalog.Domain.ValueObjects;

namespace MusicCatalog.Application.UseCases.Tracks.Commands.UpdateTrack
{
    public class UpdateTrackUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateTrackRequest> _validator;


        public UpdateTrackUseCase(IUnitOfWork unitOfWork, IValidator<UpdateTrackRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<UpdateTrackResponse> ExecuteAsync(UpdateTrackRequest request, int id, CancellationToken cancellationToken)
        {
            await ValidateRequest(request, id, cancellationToken);

            Track trackEntity = await GetTrackOrThrowIfNotExistsAsync(request, id, cancellationToken);
            
            if(request.Name is not null)
                trackEntity.ChangeName(new TrackName(request.Name));
            
            if(request.AlbumId is not null)
                await TrackReferenceSetter.SetAlbumIdIfExistsAsync(_unitOfWork, request.AlbumId.Value, trackEntity, cancellationToken, true);
            
            if(request.GenreId is not null)
                await TrackReferenceSetter.SetGenreIfExistsAsync(_unitOfWork, request.GenreId.Value, trackEntity, cancellationToken);
            
            if(request.Composer is not null)
                trackEntity.ChangeComposer(request.Composer);
            
            if(request.UnitPrice is not null)
                trackEntity.ChangeUnitPrice(request.UnitPrice.Value);


            await _unitOfWork.CommitAsync(cancellationToken);

            return UpdateTrackMappers.EntityToResponseMapper(trackEntity);           
        }

        private async Task<Track> GetTrackOrThrowIfNotExistsAsync(UpdateTrackRequest request, int id, CancellationToken cancellationToken)
        {         
            Track trackEntity = await _unitOfWork.TrackRepository.GetAsync(x => x.Id == id, cancellationToken);
            
            if(trackEntity is null)
                throw new NotFoundException("A track com o Id parametrizado não existe");
    
            return trackEntity;
        }

        private async Task ValidateRequest(UpdateTrackRequest request, int id, CancellationToken cancellationToken)
        {
            if(id <= 0)
                throw new InputValidationException("O Id parametrizado é inválido, o número deve ser inteiro é maior que 0");


            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }  
    }
}