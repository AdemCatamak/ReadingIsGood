using System.Collections.Generic;
using RIG.Shared.Domain;

namespace RIG.AccountModule.Domain.ValueObjects
{
    public class PasswordHash : ValueObject
    {
        public string Hash { get; private set; }
        public string Salt { get; private set; }

        public PasswordHash(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hash;
            yield return Salt;
        }

        public override string ToString()
        {
            return Hash;
        }
    }
}