using Microsoft.AspNetCore.Mvc;

namespace MusicCatalog.Api.Dto
{
    public class ErrorDetails : ProblemDetails
    {
        public string Code { get; init; }      
    }
}