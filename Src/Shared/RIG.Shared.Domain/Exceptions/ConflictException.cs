namespace RIG.Shared.Domain.Exceptions
{
    public abstract class ConflictException : System.Exception
    {
        public ConflictException(string message, System.Exception? innerEx = null) : base(message, innerEx)
        {
        }
    }
}