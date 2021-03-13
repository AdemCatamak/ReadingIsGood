using RIG.Shared.Domain.Exceptions;

namespace RIG.AccountModule.Domain.Exceptions
{
    public class InvalidRoleException : ValidationException
    {
        public InvalidRoleException(Roles role) : base($"'{role}' is not defined")
        {
        }
    }
}