using PlayFieldBuddy.Domain.Models;

namespace PlayFieldBuddy.Api.Services;

public interface IUserService
{
    Task<bool> DeleteUser(Guid id, CancellationToken cancellationToken);
    Task<bool> UpdateUser(User user, CancellationToken cancellationToken);
    Task<bool> AddUser(UserCreateRequest user, CancellationToken cancellationToken);
}