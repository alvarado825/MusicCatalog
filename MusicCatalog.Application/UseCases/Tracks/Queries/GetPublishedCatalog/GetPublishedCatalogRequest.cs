using MusicCatalog.Application.Common.Pagination;

namespace MusicCatalog.Application.UseCases.Tracks.Queries.GetPublishedCatalog
{
    public class GetPublishedCatalogRequest : PaginationRequestParameters
    {
        public int? ArtistId {get;init;}
        public int? GenreId {get;init;}
    }
}