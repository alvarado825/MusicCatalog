using MusicCatalog.Domain.Enums;
using MusicCatalog.Domain.Exceptions;

namespace MusicCatalog.Application.Exceptions
{
    public class InputValidationException : BaseException
    {
        public InputValidationException(string message) : base(message: message)
        {
            
        }

        public InputValidationException(ErrorCode error, string message): base(error, message)
        {
            
        }
    }
}