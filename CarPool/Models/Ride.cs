using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class Ride
    {
        public string RideId;

        //public string TotalNoOfSeats;

        public int NoOfSeatsAvailable;

        public string StartTime;

        public string EndTime;

        public string Source;

        public string Destination;

        public string CarNumber;

        public List<string> ViaPlaces;

        public List<DateTime> ViaTimings;

        public string RideProviderId;

        public List<Booking> Bookings;

        public decimal PricePerKilometer;

        //internal List<Booking> Bookings { get => bookings; set => bookings = value; }

        List<User> RideTakers;

        public Ride(string id,string rideProviderID,string carNumber, string source, string destination, string startTime, string endTime, int noOfSeats, List<string> viaPlaces, decimal amount)
        {
            this.RideId = id;
            this.RideProviderId = rideProviderID;
            this.CarNumber = carNumber;
            this.Source = source;
            this.Destination = destination;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.NoOfSeatsAvailable = noOfSeats;
            this.ViaPlaces = viaPlaces;
            this.PricePerKilometer = amount;
            Bookings = new List<Booking>();
        }

        public Ride()
        {
                
        }
    }
}
