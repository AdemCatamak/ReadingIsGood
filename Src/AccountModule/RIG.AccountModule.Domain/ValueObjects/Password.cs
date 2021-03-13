using System.Collections.Generic;
using System.Text.RegularExpressions;
using RIG.Shared.Domain;

namespace RIG.AccountModule.Domain.ValueObjects
{
    public class Password : ValueObject
    {
        // 8 characters. At least one letter and one digit
        private const string REGEX = "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$";
        public string Value { get; private set; }

        public bool IsValid() => IsValid(out string _);

        public bool IsValid(out string message)
        {
            bool isValid = Regex.IsMatch(Value, REGEX);
            message = isValid
                          ? string.Empty
                          : "Password should be at lease 8 characters and at least one of them is letter and one of them is digit";

            return isValid;
        }

        public Password(string value)
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