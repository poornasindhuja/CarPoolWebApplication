using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;
using CarPool.AppRootData;

namespace CarPool.Services
{
    public class RideTakerServices:RideServices,IRideTakerServices
    {
        public void BookRide(Booking booking)
        {
            var ride = CarPoolRootData.Rides.Find(r => r.RideId == booking.RideId);
            booking.BookingId = CarPoolRootData.Bookings.Count + 1;
            booking.StartTime = ride.StartTime.AddSeconds( GetDurationBetweenPlaces(ride.Source, booking.Source));
            booking.EndTime= ride.StartTime.AddSeconds(GetDurationBetweenPlaces(booking.Source, booking.Destination));
            booking.CostOfBooking =GetDistanceBetweenPlaces(booking.Source, booking.Destination) * ride.PricePerKilometer*booking.NumberSeatsSelected;
            booking.BookingDate = DateTime.Now;
            if (booking.NumberSeatsSelected <= ride.NoOfSeatsAvailable)
            {
                CarPoolRootData.Bookings.Add(booking);
            }  
        }

        public List<Booking> GetAllBookings(int userId)
        {
            return CarPoolRootData.Bookings.FindAll(b => b.UserId == userId);
        }

        public  List<Ride> GetAllRideOffers(int userId)
        {
            return CarPoolRootData.Rides.FindAll(r => r.DateOfRide.Date >= DateTime.Now.Date && r.RideProviderId!=userId);
        }

        public IList<Ride> SearchRides(string pickupLocation, string dropLocation,int userId)
        {
            var availableRides = new List<Ride>();

            // returns all the ride offers available from pickup location to drop location.
            foreach(Ride ride in GetAllRideOffers(userId))
            {
                if (ride.Source.ToLower() == pickupLocation.ToLower())
                {
                    if (ride.ViaPlaces.Contains(dropLocation.ToLower()) || ride.Destination.ToLower() == dropLocation.ToLower())
                    {
                        availableRides.Add(ride);
                    }
                }
                else if(ride.ViaPlaces.Contains(pickupLocation.ToLower()))
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
            return CarPoolRootData.Cars.Find(c => c.CarNo == carNumber);
        }

    }
}
