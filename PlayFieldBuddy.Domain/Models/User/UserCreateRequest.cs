using System.ComponentModel.DataAnnotations;
using PlayFieldBuddy.Domain.Enum;

namespace PlayFieldBuddy.Domain.Models;

public class UserCreateRequest
{
    [Required]
    [StringLength(15, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 15 characters.")]
    public string Username { get; set; }
    
    [Required]
    [StringLength(20, MinimumLength = 7, ErrorMessage = "Password must be between 7 and 20 characters.")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{5,20}", ErrorMessage = "Password must contain an one uppercase letter, a one lowercase letter, a one special character and one numeric character")]
    public string Password { get; set; }
    
    [Required]
    [StringLength(30, MinimumLength = 5, ErrorMessage = "Email must be between 5 and 30 characters.")]
    [RegularExpression("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\\.[a-zA-Z0-9-.]+$", ErrorMessage = "Email must be formatted correctly")]
    public string Mail { get; set; }
}