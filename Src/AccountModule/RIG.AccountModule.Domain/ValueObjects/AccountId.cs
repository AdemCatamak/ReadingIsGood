using System;
using System.Collections.Generic;
using RIG.Shared.Domain;

namespace RIG.AccountModule.Domain.ValueObjects
{
    public class AccountId : ValueObject
    {
        public Guid Value { get; private set; }

        public AccountId(Guid value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}