using PlayFieldBuddy.Domain.Models;

namespace PlayFieldBuddy.Repositories.Interfaces;

public interface IUserRepository
{
    Task AddUser(User user, CancellationToken cancellationToken);
    Task<User?> GetSingleUserById(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<User>?> GetManyUsers(CancellationToken cancellationToken);
    Task<int?> UpdateUser(User user, CancellationToken cancellationToken);
    Task DeleteUser(User user, CancellationToken cancellationToken);
    Task<User?> GetByName(string username, CancellationToken cancellationToken);
}