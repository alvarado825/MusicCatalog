using MusicCatalog.Domain.Enums;
using MusicCatalog.Domain.Exceptions;

namespace MusicCatalog.Application.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message): base(message : message)
        {
            
        }

        public NotFoundException(ErrorCode error, string message): base(error, message)
        {
            
        }
    }
}