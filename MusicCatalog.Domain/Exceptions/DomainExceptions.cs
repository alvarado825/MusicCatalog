using MusicCatalog.Domain.Enums;

namespace MusicCatalog.Domain.Exceptions
{
    public class DomainException : BaseException
    {
        public DomainException(string message) : base(message : message)
        {
            
        }

        public DomainException(ErrorCode error, string message): base(error, message)
        {
            
        }
    }
}