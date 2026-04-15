using MusicCatalog.Domain.Enums;

namespace MusicCatalog.Domain.Exceptions
{
    public class BaseException: Exception
    {
        public ErrorCode Code { get; }

        public BaseException(ErrorCode code = ErrorCode.Unknown, string message = "An error ocurred") : base(message)
        {
            Code = code;
        }
    }
}