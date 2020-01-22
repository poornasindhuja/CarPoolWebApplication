using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;
using CarPool.AppData;

namespace CarPool.Services
{
    class RideTakerServices:RideServices,IRideTakerServices
    {
        public void BookRide(int rideId,string pickUpLocation,string dropLocation,int numberOfSeatsSelected,int userId)
        {
            var ride = CarPoolData.Rides.Find(r => r.RideId == rideId);

            var startTime = ride.StartTime.AddSeconds( GetDurationBetweenPlaces(ride.Source, pickUpLocation));

            var endTime= ride.StartTime.AddSeconds(GetDurationBetweenPlaces(pickUpLocation, dropLocation));

            var costOfRide =GetDistanceBetweenPlaces(pickUpLocation, dropLocation) * ride.PricePerKilometer*numberOfSeatsSelected;

            if (numberOfSeatsSelected <= ride.NoOfSeatsAvailable)
            {
                var booking = new Booking(CarPoolData.Bookings.Count + 1, rideId, pickUpLocation, dropLocation, userId, DateTime.Now, startTime, endTime, costOfRide, numberOfSeatsSelected);

                CarPoolData.Bookings.Add(booking);
            }  
        }

        public List<Booking> GetAllBookings(int userId)
        {
            return CarPoolData.Bookings.FindAll(b => b.UserId == userId);
        }

        public  List<Ride> GetAllRideOffers(int userId)
        {
            return CarPoolData.Rides.FindAll(r => r.DateOfRide.Date >= DateTime.Now.Date && r.RideProviderId!=userId);
        }

        public List<Ride> SearchRides(string pickupLocation, string dropLocation,int userId)
        {
            var availableRides = new List<Ride>();

            // returns all the ride offers available from pickup location to drop location.
            foreach(Ride ride in GetAllRideOffers(userId))
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
            return availableRides;
        }

        public Car GetCarDetails(string carNumber)
        {
            return CarPoolData.Cars.Find(c => c.CarNo == carNumber);
        }

    }
}
