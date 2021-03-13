using RIG.AccountModule.Domain.ValueObjects;
using RIG.Shared.Domain.Exceptions;

namespace RIG.AccountModule.Domain.Exceptions
{
    public class UsernameAlreadyExistException : ConflictException
    {
        public UsernameAlreadyExistException(Username username) : base($"'{username}' already exist")
        {
        }
    }
}