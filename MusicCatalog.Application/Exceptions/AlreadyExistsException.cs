using MusicCatalog.Domain.Enums;
using MusicCatalog.Domain.Exceptions;

namespace MusicCatalog.Application.Exceptions
{
    public class AlreadyExistsException : BaseException
    {
        public AlreadyExistsException(string message):base(message : message)
        {
            
        }

        public AlreadyExistsException(ErrorCode error, string message): base(error, message)
        {
            
        }
    }
}