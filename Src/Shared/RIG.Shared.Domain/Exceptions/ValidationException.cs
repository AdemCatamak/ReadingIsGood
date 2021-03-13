namespace RIG.Shared.Domain.Exceptions
{
    public abstract class ValidationException : System.Exception
    {
        public ValidationException(string message, System.Exception? innerException = null)
            : base(message, innerException)
        {
        }
    }
}