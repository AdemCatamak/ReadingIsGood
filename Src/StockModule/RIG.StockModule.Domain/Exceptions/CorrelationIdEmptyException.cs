using System;
using RIG.Shared.Domain.Exceptions;

namespace RIG.StockModule.Domain.Exceptions
{
    public class CorrelationIdEmptyException : ValidationException
    {
        public CorrelationIdEmptyException() : base("Correlation id should not be empty")
        {
        }
    }
}