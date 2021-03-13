using System;
using System.Collections.Generic;
using RIG.Shared.Domain;

namespace RIG.AccountModule.Domain.ValueObjects
{
    public class AccessToken : ValueObject
    {
        public AccountId AccountId { get; private set; }
        public string Value { get; private set;}
        public DateTime ExpireAt { get; private set;}

        public AccessToken(AccountId accountId, string value, DateTime expireAt)
        {
            AccountId = accountId;
            Value = value;
            ExpireAt = expireAt;
        }

        public override string ToString()
        {
            return Value;
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return AccountId;
            yield return ExpireAt;
        }
    }
}