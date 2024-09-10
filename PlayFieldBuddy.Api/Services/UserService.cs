using System.Security.Cryptography;
using System.Text;
using PlayFieldBuddy.Domain.Enum;
using PlayFieldBuddy.Domain.Models;
using PlayFieldBuddy.Repositories.Interfaces;

namespace PlayFieldBuddy.Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        var foundUser = await _userRepository.GetSingleUserById(id, cancellationToken);
    
        if (foundUser is null)
        {
            return false;
        }
    
        await _userRepository.DeleteUser(foundUser, cancellationToken);
        return true;
    }

    public async Task<bool> UpdateUser(User user, CancellationToken cancellationToken)
    {
        var foundUser = await _userRepository.GetSingleUserById(user.Id, cancellationToken);
        
        if (foundUser is null)
        {
            return false;
        }

        foundUser.Username = user.Username;
        foundUser.JoinedGames = user.JoinedGames;
        foundUser.OwnedGames = user.OwnedGames;
        foundUser.Password = HashPassword(user.Password);
        foundUser.Mail = user.Mail;
        foundUser.Role = Role.User;
        
        await _userRepository.UpdateUser(foundUser, cancellationToken);
        return true;
    }
    
    public async Task<bool> AddUser(UserCreateRequest user, CancellationToken cancellationToken)
    {
        var foundUser = await _userRepository.GetByName(user.Username, cancellationToken);
        
        if (foundUser is not null)
        {
            return false;
        }

        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            Username = user.Username,
            Mail = user.Mail,
            Password = HashPassword(user.Password),
            Role = Role.User,
            JoinedGames = new List<Game>(),
            OwnedGames = new List<Game>()
        };
        
        await _userRepository.AddUser(newUser, cancellationToken);
        return true;
    }

    private static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            
            byte[] hash = sha256.ComputeHash(bytes);
            StringBuilder result = new StringBuilder();
            foreach (byte b in hash)
            {
                result.Append(b.ToString("x2"));
            }

            return result.ToString();
        }
    }
}