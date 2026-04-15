using MusicCatalog.Application.Exceptions;
using MusicCatalog.Application.Interfaces.Persistence;
using MusicCatalog.Domain.Entities;
using MusicCatalog.Domain.Enums;

namespace MusicCatalog.Application.UseCases.Tracks.Commands.PublishTrack
{
    public class PublishTrackUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PublishTrackUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PublishTrackResponse> ExecuteAsync (int id, CancellationToken cancellationToken)
        {
            Track trackEntity = await GetTrackOrThrowAsync(id, cancellationToken);

            if(trackEntity.TrackStatus == TrackStatusEnum.Draft)
            {
                trackEntity.Publish();
              
                await _unitOfWork.CommitAsync(cancellationToken);
            }
            else
            {
                throw new BusinessRuleException(ErrorCode.TrackIsAlreadyPublished, "Track is already published.");
            }

            return PublishTrackMappers.EntityToResponseMapper(trackEntity);
        }      

        public async Task<Track> GetTrackOrThrowAsync(int idRequest, CancellationToken cancellationToken)
        {
            Track trackEntity = await _unitOfWork.TrackRepository.GetAsync(x => x.Id == idRequest, cancellationToken);

            if(trackEntity is null)
                throw new NotFoundException($"Track not found.");


            return trackEntity;
        } 
    }
}