using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class Booking
    {
        public string bookingId;

        public string rideId;

        public string source;

        public string destination;

        public string userId;

        public DateTime bookingDate;

        public bool status;

        public string startTime;

        public string endTime;

        public Booking(string bookingId, string rideId, string source, string destination, string userId, DateTime bookingDate)
        {
            this.bookingId = bookingId;
            this.rideId = rideId;
            this.source = source;
            this.destination = destination;
            this.userId = userId;
            this.bookingDate = bookingDate;
        }
    }
}
