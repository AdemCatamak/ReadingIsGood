using System;

namespace RIG.Shared.Domain.Exceptions
{
    public abstract class IntegrationException : Exception
    {
        protected IntegrationException(string friendlyMessage, Exception? innerEx = null)
            : base(friendlyMessage, innerEx)
        {
        }
    }
}