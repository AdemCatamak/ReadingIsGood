using RIG.Shared.Domain.Pagination.Exceptions;

namespace RIG.Shared.Domain.Pagination
{
    public abstract class PaginatedRequest
    {
        private int _offset = 0;

        public int Offset
        {
            get => _offset;
            set
            {
                if (value < 0) throw new NegativeOffsetException(value);
                _offset = value;
            }
        }

        private int _limit = 1;

        public int Limit
        {
            get => _limit;
            set
            {
                if (value < 0) throw new NegativeLimitException(value);
                _limit = value;
            }
        }

        public PaginatedRequest()
        {
        }

        public PaginatedRequest(int offset, int limit)
        {
            Offset = offset;
            Limit = limit;
        }
    }
}