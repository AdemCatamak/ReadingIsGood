using RIG.Shared.Domain.Pagination;

namespace RIG.WebApi.Modules.ProductModule.Controllers.Requests
{
    public class GetBookCollectionHttpRequest : PaginatedRequest
    {
        public string? PartialBookName { get; set; } = null;
        public string? PartialAuthorName { get; set; } = null;
    }
}