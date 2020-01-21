using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;
using CarPool.AppData;

namespace CarPool.Services
{
    class RideTakerServices
    {
        readonly RideServices rideServices;

        public RideTakerServices()
        {
            rideServices = new RideServices();
        }

        public void BookRide(int rideId,string pickUpLocation,string dropLocation)
        {
            var ride = Database.Rides.Find(r => r.RideId == rideId);

            var startTime = ride.StartTime.AddSeconds( rideServices.GetDurationBetweenPlaces(ride.Source, pickUpLocation));

            var endTime= ride.StartTime.AddSeconds(rideServices.GetDurationBetweenPlaces(pickUpLocation, dropLocation));

            var costOfRide = rideServices.GetDistanceBetweenPlaces(pickUpLocation, dropLocation) * ride.PricePerKilometer;

            var booking = new Booking(Database.Bookings.Count+1, rideId, pickUpLocation, dropLocation, UserServices.CurrentUser.UserId, DateTime.Now,startTime,endTime,costOfRide);

            Database.Bookings.Add(booking);
        }

        public List<Booking> GetAllBookings()
        {
            return Database.Bookings.FindAll(b => b.UserId == UserServices.CurrentUser.UserId);
        }

        public  List<Ride> GetAllRideOffers()
        {
            return Database.Rides.FindAll(r => r.DateOfRide >= DateTime.Now && r.RideProviderId!=UserServices.CurrentUser.UserId);
        }

        public List<Ride> SearchRides(string pickupLocation, string dropLocation)
        {
            List<Ride> availableRides = new List<Ride>();
            // returns all the ride offers available from pickup location to drop location.
            foreach(Ride ride in GetAllRideOffers())
            {
                if (ride.Source == pickupLocation)
                {
                    if (ride.ViaPlaces.Contains(dropLocation) || ride.Destination == dropLocation)
                    {
                        availableRides.Add(ride);
                    }
                }
                else if(ride.ViaPlaces.Contains(pickupLocation))
                {
                    if((ride.ViaPlaces.Contains(dropLocation)&& ride.ViaPlaces.IndexOf(dropLocation)>ride.ViaPlaces.IndexOf(pickupLocation))
                        || ride.Destination == dropLocation)
                    {
                        availableRides.Add(ride);
                    }
                }   
            }
            //return RideServices.Rides.FindAll(r => r.ViaPlaces.Contains(pickupLocation)||r.Source==pickupLocation && r.ViaPlaces.Contains(dropLocation)||r.Destination==dropLocation && r.ViaPlaces.IndexOf(pickupLocation)<r.ViaPlaces.IndexOf(dropLocation));   
            return availableRides;
        }

        internal Car GetCarDetails(string carNumber)
        {
            return Database.Cars.Find(c => c.CarNo == carNumber);
        }

    }
}
