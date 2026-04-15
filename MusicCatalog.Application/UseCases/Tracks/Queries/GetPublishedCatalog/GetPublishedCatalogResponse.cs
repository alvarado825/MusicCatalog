namespace MusicCatalog.Application.UseCases.Tracks.Queries.GetPublishedCatalog
{
    public class GetPublishedCatalogResponse
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public IReadOnlyCollection<PublicCatalogItemDto> Items { get; set; } = new List<PublicCatalogItemDto>();
    }

    public class PublicCatalogItemDto
    {//Atnção foram incluidos campos aqui, criar um TrackDetailDTO e herdar em CreateTrack, UpdateTrack, aqui vamos manter um dto simples
    //Criar um GetTrack com filtros para inativos e drafts
    //Decisão todos os endpoints retornarão apenas tracks ativas criar metodo que faz essa consulta espcifica em trackRepository e corrigir nos outros use cases
    
        public int TrackId { get; set; }
        public string TrackName { get; set; }
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public int? AlbumId { get; set; }  
        public string AlbumName { get; set; }
        public int? GenreId { get; set; }
        public string? GenreName { get; set; }
    }
}