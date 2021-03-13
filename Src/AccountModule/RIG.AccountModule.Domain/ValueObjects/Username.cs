using System.Collections.Generic;
using RIG.Shared.Domain;

namespace RIG.AccountModule.Domain.ValueObjects
{
    public class Username : ValueObject
    {
        public string Value { get; private set; }

        public bool IsValid() => IsValid(out string _);

        public bool IsValid(out string message)
        {
            var isValid = Value.Length > 2;
            message = isValid
                          ? string.Empty
                          : "Username length should be at least three digit";

            return isValid;
        }

        public Username(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}