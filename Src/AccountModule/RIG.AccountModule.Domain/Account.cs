using System;
using System.Threading;
using RIG.AccountModule.Domain.Exceptions;
using RIG.AccountModule.Domain.Rules;
using RIG.AccountModule.Domain.Services;
using RIG.AccountModule.Domain.ValueObjects;

namespace RIG.AccountModule.Domain
{
    public class Account
    {
        public AccountId Id { get; private set; } = null!;
        public Username Username { get; private set; } = null!;
        public PasswordHash PasswordHash { get; private set; } = null!;
        public Name Name { get; private set; } = null!;

        private Account()
        {
            // Only for EF
        }

        private Account(Username username, PasswordHash passwordHash, Name name)
            : this(new AccountId(Guid.NewGuid()), username, passwordHash, name)
        {
        }

        private Account(AccountId id, Username username, PasswordHash passwordHash, Name name)
        {
            Id = id;
            Username = username;
            PasswordHash = passwordHash;
            Name = name;
        }

        public static Account Create(Username username, Password password, Name name, IUsernameUniqueChecker usernameUniqueChecker, IPasswordHasher passwordHasher, CancellationToken cancellationToken = default)
        {
            if (!username.IsValid(out string usernameErrorMessage)) throw new UsernameNotValidException(username, usernameErrorMessage);
            if (!password.IsValid(out string passwordErrorMessage)) throw new PasswordNotValidException(password, passwordErrorMessage);
            if (!usernameUniqueChecker.CheckAsync(username, cancellationToken).ConfigureAwait(continueOnCapturedContext: false).GetAwaiter().GetResult())
                throw new UsernameAlreadyExistException(username);

            PasswordHash passwordHash = passwordHasher.Hash(password);
            Account account = new Account(username, passwordHash, name);
            return account;
        }

        public AccessToken CreateAccessToken(Password password, IPasswordHasher passwordHasher, IAccessTokenGenerator accessTokenGenerator)
        {
            PasswordHash passwordHash = passwordHasher.Hash(password, PasswordHash.Salt);
            if (!Equals(PasswordHash, passwordHash)) throw new PasswordNotMatchException(Username, password);

            AccessToken accessToken = accessTokenGenerator.Generate(Id);
            return accessToken;
        }
    }
}