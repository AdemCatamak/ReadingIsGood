namespace RIG.WebApi.Controllers.ProductModule.Requests
{
    public class PostBookHttpRequest
    {
        public string BookName { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
    }
}