using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Data.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        [Required]
        public string PetName { get; set; }

        [StringLength(maximumLength: 16, MinimumLength = 4, ErrorMessage = "Password Length should consists of minimum 4 characters")]
        public string Password { get; set; }
    }
}
