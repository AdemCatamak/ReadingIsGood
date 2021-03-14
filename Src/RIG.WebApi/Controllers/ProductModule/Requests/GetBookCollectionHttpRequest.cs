using RIG.Shared.Domain.Pagination;

namespace RIG.WebApi.Controllers.ProductModule.Requests
{
    public class GetBookCollectionHttpRequest : PaginatedRequest
    {
        public string? PartialBookName { get; set; } = null;
        public string? PartialAuthorName { get; set; } = null;
    }
}