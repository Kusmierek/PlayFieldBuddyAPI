using PlayFieldBuddy.Domain.Enum;


namespace PlayFieldBuddy.Domain.Models;

public class Pitch
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string adress { get; set; }

    public PitchType PitchType { get; set; }
    public ICollection<Game> Games { get; set; } = new List<Game>();

}