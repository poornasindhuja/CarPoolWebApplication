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

        public string Password { get; set; }

        public List<Car> Cars { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<Ride> Rides { get; set; }
    }
}
