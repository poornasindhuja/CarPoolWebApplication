using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int RideId { get; set; }

        public string Source { get; set; }

        public string Destination { get; set; }

        public int UserId { get; set; }

        public DateTime BookingDate { get; set; }

        public BookingStatus Status { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public decimal CostOfBooking { get; set; }

        public Booking(int bookingId, int rideId, string source, string destination, int userId, DateTime bookingDate,DateTime startTime,DateTime endTime,decimal cost)
        {
            this.BookingId = bookingId;
            this.RideId = rideId;
            this.Source = source;
            this.Destination = destination;
            this.UserId = userId;
            this.BookingDate = bookingDate;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.CostOfBooking = cost;
        }
    }
}
