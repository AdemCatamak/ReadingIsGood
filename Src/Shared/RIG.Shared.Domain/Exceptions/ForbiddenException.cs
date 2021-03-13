namespace RIG.Shared.Domain.Exceptions
{
    public class ForbiddenException : System.Exception
    {
        public ForbiddenException(string? message = null) : base(message)
        {
        }
    }
}