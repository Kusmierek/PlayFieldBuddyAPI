using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayFieldBuddy.Domain.Models;
public class GameUpdateRequest
{

    public DateTime GameDate { get; set; }


    [Range(1, 30, ErrorMessage = "Players Limit must be greater than 0 and less than 31")]
    public int PlayersLimit { get; set; }

    public Guid UserId { get; set; }


}
