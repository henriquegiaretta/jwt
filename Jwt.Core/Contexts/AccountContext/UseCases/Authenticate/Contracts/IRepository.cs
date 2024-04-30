using Jwt.Core.Contexts.AccountContext.Entities;

namespace Jwt.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;

public interface IRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}