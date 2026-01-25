using PlayFieldBuddy.Domain.Models;

public class GameDto
{
    public Guid Id { get; set; }
    public DateTime GameDate { get; set; }
    public int PlayersLimit { get; set; } = 22;

    public PitchDto Pitch { get; set; }
    public UserDto Owner { get; set; }
    public List<UserDto> Users { get; set; } = new List<UserDto>();
}