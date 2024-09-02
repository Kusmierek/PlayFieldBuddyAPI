using System.ComponentModel.DataAnnotations;
using PlayFieldBuddy.Domain.Enum;

namespace PlayFieldBuddy.Domain.Models;

public class User
{
    
    public Guid Id { get; set; }

    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public string Mail { get; set; }
    
    public Role Role { get; set; } = Role.User;

    public ICollection<Game> Games { get; set; } = new List<Game>();
}