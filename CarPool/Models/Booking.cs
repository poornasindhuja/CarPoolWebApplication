using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class Booking
    {
        public string BookingId { get; set; }

        public string RideId { get; set; }

        public string Source { get; set; }

        public string Destination { get; set; }

        public string UserId { get; set; }

        public DateTime BookingDate { get; set; }

        public bool Status { get; set; }

        public bool DoesProviderViewed { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public Booking(string bookingId, string rideId, string source, string destination, string userId, DateTime bookingDate)
        {
            this.BookingId = bookingId;
            this.RideId = rideId;
            this.Source = source;
            this.Destination = destination;
            this.UserId = userId;
            this.BookingDate = bookingDate;
        }
    }
}
