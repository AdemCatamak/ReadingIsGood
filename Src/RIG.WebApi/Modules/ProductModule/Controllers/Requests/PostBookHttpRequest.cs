namespace RIG.WebApi.Modules.ProductModule.Controllers.Requests
{
    public class PostBookHttpRequest
    {
        public string BookName { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
    }
}