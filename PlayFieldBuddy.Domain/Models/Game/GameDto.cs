
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayFieldBuddy.Domain.Models;

    public class GameDto
    {
        public Guid Id { get; set; }
        public DateTime GameDate { get; set; }
        public int PlayersLimit { get; set; } = 22;

        public List<UserDto> Users { get; set; }
    }

