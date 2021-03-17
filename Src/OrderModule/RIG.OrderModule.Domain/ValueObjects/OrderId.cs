using System;
using System.Collections.Generic;
using RIG.Shared.Domain;

namespace RIG.OrderModule.Domain.ValueObjects
{
    public class OrderId : ValueObject
    {
        public Guid Value { get; private set; }

        public OrderId(Guid value)
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