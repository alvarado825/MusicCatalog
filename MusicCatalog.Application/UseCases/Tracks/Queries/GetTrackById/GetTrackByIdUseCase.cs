using Microsoft.EntityFrameworkCore;
using MusicCatalog.Application.Exceptions;
using MusicCatalog.Application.Interfaces.Persistence;
using MusicCatalog.Domain.Enums;

namespace MusicCatalog.Application.UseCases.Tracks.Queries.GetTrackById
{
    public class GetTrackByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTrackByIdUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetTrackByIdResponse> ExecuteAsync(int requestId, CancellationToken cancellationToken)
        {
            ValidateRequest(requestId);

            var track = await (

                from t in _unitOfWork.TrackRepository.Query()

                join a in _unitOfWork.ArtistRepository.Query()
                    on t.ArtistId equals a.Id

                join al in _unitOfWork.AlbumRepository.Query()
                    on t.AlbumId equals al.Id into albumGroup

                from album in albumGroup.DefaultIfEmpty()

                join ge in _unitOfWork.GenreRepository.Query()
                    on t.GenreId equals ge.Id into genreGroup

                from genre in genreGroup.DefaultIfEmpty()

                where t.Id == requestId
                
                select new GetTrackByIdResponse
                {
                    Id = t.Id,
                    Name = t.Name.Value,
                    GenreId = t.GenreId == null ? null : t.GenreId,
                    GenreName = genre == null ? null : genre.Name.Value,
                    ArtistId = t.ArtistId,
                    ArtistName = a.Name.Value,
                    AlbumId = t.AlbumId == null ? null : t.AlbumId,
                    AlbumName = album != null ? album.Name.Value : null,
                    Duration = t.Duration,
                    Bytes = t.Bytes,
                    Price = t.UnitPrice,
                    IsActive = t.IsActive,
                    Status = t.TrackStatus.ToString()
                }

            ).FirstOrDefaultAsync(cancellationToken);

            if (track is null)
                throw new NotFoundException(ErrorCode.TrackNotFound, $"Track {requestId} not found");

            if (!track.IsActive)
                throw new TrackInactiveException(ErrorCode.TrackInactive, $"Track {requestId} is inactive");

            return track;
        }

        private static void ValidateRequest(int requestId)
        {
            if (requestId <= 0)
                throw new InputValidationException("TrackId must be greather than zero");
        }
    }
}