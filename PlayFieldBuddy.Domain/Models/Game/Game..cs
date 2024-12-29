namespace PlayFieldBuddy.Domain.Models;

public class Game
{
    public Guid Id { get; set; }
    public DateTime GameDate { get; set; }
    public int PlayersLimit { get; set; } = 22;
    public ICollection<User> Users { get; set; } = new List<User>();
    public Pitch Pitch { get; set; }
    public User Owner { get; set; }

}