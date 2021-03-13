using RIG.Shared.Domain.Exceptions;

namespace RIG.Shared.Domain.Pagination.Exceptions
{
    public class RangeExceedException : ValidationException
    {
        public RangeExceedException(int maxLimit, int limit)
            : base($"Limit value which is {limit} should be max {maxLimit}")
        {
        }
    }
}