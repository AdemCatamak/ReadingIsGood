namespace RIG.Shared.Domain.Exceptions
{
    public class AuthException : System.Exception
    {
        public AuthException(string message, System.Exception? innerEx = null) : base(message, innerEx)
        {
        }
    }
}