using MusicCatalog.Domain.Enums;

namespace MusicCatalog.Domain.Exceptions
{
    public class DomainRuleViolationException : BaseException
    {
        public DomainRuleViolationException(string message) : base(message : message)
        {
            
        }

        public DomainRuleViolationException(ErrorCode error, string message): base(error, message)
        {
            
        }
    }
}