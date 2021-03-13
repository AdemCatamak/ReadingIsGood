using RIG.Shared.Domain.Exceptions;

namespace RIG.Shared.Domain.Pagination.Exceptions
{
    public class NegativeLimitException : ValidationException
    {
        public NegativeLimitException(int limit)
            : base($"Negative limit is not acceptable [{limit}]")
        {
        }
    }
}