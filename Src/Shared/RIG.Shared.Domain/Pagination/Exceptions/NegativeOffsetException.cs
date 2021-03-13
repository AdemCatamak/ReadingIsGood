using RIG.Shared.Domain.Exceptions;

namespace RIG.Shared.Domain.Pagination.Exceptions
{
    public class NegativeOffsetException : ValidationException
    {
        public NegativeOffsetException(int offset) : base($"Negative offset is not acceptable [{offset}]")
        {
        }
    }
}