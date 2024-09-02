using System.ComponentModel.DataAnnotations;
using PlayFieldBuddy.Domain.Enum;

namespace PlayFieldBuddy.Domain.Models;

public class Pitch
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string adress { get; set; }

    public PitchType PitchType { get; set; } = PitchType.Uncovered;
    public ICollection<Game> Games { get; set; } = new List<Game>();

}