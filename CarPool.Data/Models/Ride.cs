using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Data.Models
{
    public class Ride
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RideId { get; set; }

        public int NoOfSeatsAvailable { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [Required]
        public string Source { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public string CarNumber { get; set; }

        public string ViaPlaces { get; set; }

        public int RideProviderId { get; set; }

        [Required]
        [Range(0.1, 20)]
        public decimal PricePerKilometer { get; set; }

        [Required]
        public DateTime DateOfRide { get; set; }

        List<Booking> Bookings { get; set; }
        public User User { get; set; }
        public Car Car { get; set; }
    }
}
