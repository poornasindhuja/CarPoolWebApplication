using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Data.Models
{
    public class Booking
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }

        [Required]
        public int RideId { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "Starting Location Must be there")]
        public string Source { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 5)]
        public string Destination { get; set; }

        [Required]
        public int UserId { get; set; }

        //[CurrentDate]
        [Required]
        public DateTime BookingDate { get; set; }

        public short Status { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public decimal CostOfBooking { get; set; }

        public int NumberSeatsSelected { get; set; }
    }
}
