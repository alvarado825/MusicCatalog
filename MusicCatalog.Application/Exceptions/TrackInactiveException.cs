using MusicCatalog.Domain.Enums;
using MusicCatalog.Domain.Exceptions;

namespace MusicCatalog.Application.Exceptions
{
    public class TrackInactiveException : BaseException
    {
        public TrackInactiveException(string message): base(message : message)
        {
            
        }

        public TrackInactiveException(ErrorCode error, string message): base(error, message)
        {
            
        }
    }
}