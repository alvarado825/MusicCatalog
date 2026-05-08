using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.Application.UseCases.Tracks.Commands.CreateTrack;
using MusicCatalog.Application.UseCases.Tracks.Commands.DeactivateTrack;
using MusicCatalog.Application.UseCases.Tracks.Commands.PublishTrack;
using MusicCatalog.Application.UseCases.Tracks.Commands.UpdateTrack;
using MusicCatalog.Application.UseCases.Tracks.Queries.GetPublishedCatalog;
using MusicCatalog.Application.UseCases.Tracks.Queries.GetTrackById;

namespace MusicCatalog.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TracksController : ControllerBase
    {
        private readonly CreateTrackUseCase _createTrackUseCase;
        private readonly UpdateTrackUseCase _updateTrackUseCase;
        private readonly PublishTrackUseCase _publishTrackUseCase;
        private readonly DeactivateTrackUseCase _deactivateTrackUseCase;
        private readonly GetPublishedCatalogUseCase _getPublishedCatalogUseCase;
        private readonly GetTrackByIdUseCase _getTrackByIdUseCase;

        public TracksController(
            CreateTrackUseCase createTrackUseCase,
            UpdateTrackUseCase updateTrackUseCase,
            PublishTrackUseCase publishTrackUseCase,
            DeactivateTrackUseCase deactivateTrackUseCase,
            GetPublishedCatalogUseCase getPublishedCatalogUseCase,
            GetTrackByIdUseCase getTrackByIdUseCase)
        {
            _createTrackUseCase = createTrackUseCase;
            _updateTrackUseCase = updateTrackUseCase;
            _publishTrackUseCase = publishTrackUseCase;
            _deactivateTrackUseCase = deactivateTrackUseCase;
            _getPublishedCatalogUseCase = getPublishedCatalogUseCase;
            _getTrackByIdUseCase = getTrackByIdUseCase;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetTrackByIdResponse>> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _getTrackByIdUseCase.ExecuteAsync(id, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<GetPublishedCatalogResponse>> GetPublishedCatalog([FromQuery] GetPublishedCatalogRequest queryStringRequest, CancellationToken cancellationToken)
        {
            var result = await _getPublishedCatalogUseCase.ExecuteAsync(queryStringRequest, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CreateTrackResponse>> Create([FromBody] CreateTrackRequest request, CancellationToken cancellationToken)
        {
            var result = await _createTrackUseCase.ExecuteAsync(request, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UpdateTrackResponse>> Update([FromRoute] int id, [FromBody] UpdateTrackRequest request, CancellationToken cancellationToken)
        {
            var result = await _updateTrackUseCase.ExecuteAsync(request, id, cancellationToken);

            return Ok(result);
        }

        [HttpPatch("{id:int}/publish")]
        public async Task<ActionResult<PublishTrackResponse>> Publish([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _publishTrackUseCase.ExecuteAsync(id, cancellationToken);

            return Ok(result);
        }

        [HttpPatch("{id:int}/deactivate")]
        public async Task<ActionResult<DeactivateTrackResponse>> Deactivate([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _deactivateTrackUseCase.ExecuteAsync(id, cancellationToken);

            return Ok(result);
        }
    }
}