using System;
using RIG.Shared.Domain.Exceptions;

namespace RIG.StockModule.Domain.Exceptions
{
    public class StockAlreadyExistException : ConflictException
    {
        public StockAlreadyExistException(string productId) : base($"Stock already initialized for productId:{productId}")
        {
        }
    }
}