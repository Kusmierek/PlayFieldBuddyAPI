using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayFieldBuddy.Domain.Models
{
    public class GameCreateRequest
    {
        public DateTime GameDate { get; set; }

        public int PlayersLimit { get; set; }

        public Pitch Pitch { get; set; }

    }
}
