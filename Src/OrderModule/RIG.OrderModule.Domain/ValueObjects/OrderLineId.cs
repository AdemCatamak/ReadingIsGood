using System;
using System.Collections.Generic;
using RIG.Shared.Domain;

namespace RIG.OrderModule.Domain.ValueObjects
{
    public class OrderLineId : ValueObject
    {
        public Guid Value { get; private set; }

        public OrderLineId(Guid value)
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