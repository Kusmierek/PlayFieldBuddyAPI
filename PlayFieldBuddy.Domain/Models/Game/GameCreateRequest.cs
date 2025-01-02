using System;
using System.ComponentModel.DataAnnotations;

namespace PlayFieldBuddy.Domain.Models
{
    public class GameCreateRequest
    {
        [Required]
        public Guid OwnerId { get; set; }
        
        [Required]
        public Guid PitchId { get; set; }

        public Guid UserId { get; set; }
        
        [Required]   
        public DateTime GameDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "PlayersLimit must be greater than 0")]
        public int PlayersLimit { get; set; }
    }
}
