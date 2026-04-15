namespace MusicCatalog.Application.UseCases.Tracks.Queries.GetPublishedCatalog
{
    public static class GetPublishedCatalogMappers
    {
        public static GetPublishedCatalogResponse ToResponseMapper (GetPublishedCatalogRequest request, int totalTrackItems, int totalPages, List<PublicCatalogItemDto> catalogItems)
        {
            return new GetPublishedCatalogResponse
            {
                Page = request.Page,
                PageSize = request.PageSize,
                TotalItems = totalTrackItems,
                TotalPages = totalPages,
                Items = catalogItems
            };
        }
    }
}