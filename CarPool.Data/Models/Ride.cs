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

        //[AvailableSeats]
        public int NoOfSeatsAvailable { get; set; }

        [Required(ErrorMessage = "Strating time should not be empty")]
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Starting location can not be empty")]
        public string Source { get; set; }

        [Required(ErrorMessage = "Destination location can not be empty")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "Car number should not be empty")]
        public string CarNumber { get; set; }

        public string ViaPlaces { get; set; }

        public int RideProviderId { get; set; }

        [Required(ErrorMessage = "Cost per killometer should not be empty")]
        [Range(0.1, 20, ErrorMessage = "You can charge upto 20 rupees per killometer only")]
        public decimal PricePerKilometer { get; set; }

        [Required(ErrorMessage = "Date of ride should not be empty")]
        public DateTime DateOfRide { get; set; }
    }
}
