using MusicCatalog.Domain.Enums;
using MusicCatalog.Domain.Exceptions;

namespace MusicCatalog.Application.Exceptions
{
    public class BusinessRuleException : BaseException
    {
        public BusinessRuleException(string message) : base(message: message)
        {
            
        }

        public BusinessRuleException(ErrorCode error, string message): base(error, message)
        {
            
        }
    }
}