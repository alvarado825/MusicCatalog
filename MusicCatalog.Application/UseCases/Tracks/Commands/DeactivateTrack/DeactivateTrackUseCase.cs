using MusicCatalog.Application.Exceptions;
using MusicCatalog.Application.Interfaces.Persistence;
using MusicCatalog.Domain.Entities;

namespace MusicCatalog.Application.UseCases.Tracks.Commands.DeactivateTrack
{
    public class DeactivateTrackUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateTrackUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DeactivateTrackResponse> ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            ValidateRequest(id);

            Track trackEntity = await GetTrackOrThrowAsync(id, cancellationToken);

            trackEntity.Deactivate();

            await _unitOfWork.CommitAsync(cancellationToken);
            
            return DeactivateTrackMappers.EntityToResponseMapper(trackEntity);

        }

        private async Task<Track> GetTrackOrThrowAsync(int id, CancellationToken cancellationToken)
        {
            Track trackEntity = await _unitOfWork.TrackRepository.GetAsync(x => x.Id == id, cancellationToken);
            
            if(trackEntity is null)
                throw new NotFoundException("Track not found.");

    
            return trackEntity;

        }

         private static void ValidateRequest(int requestId)
        {
            if (requestId <= 0)
                throw new InputValidationException("Id must be greather than 0.");
        }
    }
}