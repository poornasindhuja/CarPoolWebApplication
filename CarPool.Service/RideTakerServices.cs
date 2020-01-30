using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;
using CarPool.AppData;
using CarPool.DataValidations;

namespace CarPool.Services
{
    public class RideTakerServices:RideServices,IRideTakerServices
    {
        public GenericValidator genericValidator;
        public RideTakerServices()
        {
            genericValidator = new GenericValidator();
        }
        public bool BookRide(Booking booking)
        {
            var ride = CarPoolData.Rides.Find(r => r.RideId == booking.RideId);
            booking.BookingId = CarPoolData.Bookings.Count + 1;
            booking.StartTime = ride.StartTime.AddSeconds( GetDurationBetweenPlaces(ride.Source, booking.Source));
            booking.EndTime= ride.StartTime.AddSeconds(GetDurationBetweenPlaces(booking.Source, booking.Destination));
            var placesList = new List<string>
            {
                ride.Source
            };
            placesList.AddRange(ride.ViaPlaces);
            placesList.Add(ride.Destination);
            booking.CostOfBooking = GetDistanceBetweenPlaces(booking.Source, booking.Destination, placesList.GetRange(placesList.IndexOf(booking.Source) + 1, placesList.IndexOf(booking.Destination) - 1)) * ride.PricePerKilometer * booking.NumberSeatsSelected;
            booking.BookingDate = DateTime.Now;
            if (booking.NumberSeatsSelected <= ride.NoOfSeatsAvailable)
            {
                CarPoolData.Bookings.Add(booking);
                return true;         
            }
            return false;
        }

        public List<Booking> GetAllBookings(int userId)
        {
            return CarPoolData.Bookings.FindAll(b => b.UserId == userId);
        }

        public  List<Ride> GetAllRideOffers(int userId)
        {
            return CarPoolData.Rides.FindAll(r => r.DateOfRide.Date >= DateTime.Now.Date && r.RideProviderId!=userId);
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
            return CarPoolData.Cars.Find(c => c.CarNo == carNumber);
        }

    }
}
