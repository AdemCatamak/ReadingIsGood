using System;
using System.Collections.Generic;
using RIG.Shared.Domain;

namespace RIG.ProductModule.Domain.ValueObjects
{
    public class BookId : ValueObject
    {
        public Guid Value { get; private set; }

        public BookId(Guid value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}