using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.Application.UseCases.Albums.Command.CreateAlbum;

namespace MusicCatalog.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly CreateAlbumUseCase _createAlbumUseCase;

        public AlbumsController(CreateAlbumUseCase createAlbumUseCase)
        {
            _createAlbumUseCase = createAlbumUseCase;
        }

        [HttpPost]
        public async Task<ActionResult<CreateAlbumResponse>> CreateAlbumWithOptionalArtist([FromBody] CreateAlbumRequest request, CancellationToken cancellationToken)
        {
            CreateAlbumResponse result = await _createAlbumUseCase.ExecuteAsync(request, cancellationToken);

            return Ok(result);
        }
    }
}