namespace PlayFieldBuddy.Domain.Models;

public class Game
{
    public Guid Id { get; set; }
    
    public DateTime GameDate { get; set; }
    
    public int PlayersLimit { get; set; }
    
    public ICollection<User> Users { get; set; } = new List<User>();
}