using MusicCatalog.Domain.Enums;
using MusicCatalog.Domain.Exceptions;

namespace MusicCatalog.Application.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(message : message)
        {
            
        }

        public BadRequestException(ErrorCode error, string message): base(error, message)
        {
            
        }
    }
}