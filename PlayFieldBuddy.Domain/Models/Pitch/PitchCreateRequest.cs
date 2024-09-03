using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayFieldBuddy.Domain.Models
{
    public class PitchCreateRequest
    {
        [Required]

        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Name can only contain letters, numbers, and spaces.")]
        public string Name { get; set; }


        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s,.-]+$", ErrorMessage = "Address can only contain letters, numbers, spaces, commas, periods, and hyphens.")]
        public string Address { get; set; }

  
    }
}
