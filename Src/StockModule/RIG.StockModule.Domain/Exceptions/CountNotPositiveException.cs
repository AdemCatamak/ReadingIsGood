using System;
using RIG.Shared.Domain.Exceptions;

namespace RIG.StockModule.Domain.Exceptions
{
    public class CountNotPositiveException : ValidationException
    {
        public CountNotPositiveException() : base("Count should be positive number")
        {
        }
    }
}