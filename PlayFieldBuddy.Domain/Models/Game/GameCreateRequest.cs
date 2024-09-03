using System;
using System.ComponentModel.DataAnnotations;

namespace PlayFieldBuddy.Domain.Models
{
    public class GameCreateRequest
    {
        [Required]
       
        public DateTime GameDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "PlayersLimit must be greater than 0")]
        public int PlayersLimit { get; set; }

        [Required]
        public Pitch Pitch { get; set; }
    }
}
