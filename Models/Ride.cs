using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class Ride
    {
        public int RideId { get; set; }

        public int NoOfSeatsAvailable { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Source { get; set; }

        public string Destination { get; set; }

        public string CarNumber { get; set; }

        public List<string> ViaPlaces { get; set; }

        public List<DateTime> ViaTimings { get; set; }

        public int RideProviderId { get; set; }

        public decimal PricePerKilometer { get; set; }

        public DateTime DateOfRide { get; set; }

        public Ride(int id,int rideProviderID,string carNumber, string source, string destination, DateTime startTime, DateTime endTime, int noOfSeats, List<string> viaPlaces, decimal amount,DateTime dateOfRide)
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
            this.DateOfRide = dateOfRide;
        }
        public Ride()
        {

        }
    }
}
