using System.ComponentModel.DataAnnotations;
using PlayFieldBuddy.Domain.Enum;

namespace PlayFieldBuddy.Domain.Models;

public class UserCreateRequest
{
    [Required]
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public string Mail { get; set; }
    
    public Role Role { get; set; } = Role.User;
}