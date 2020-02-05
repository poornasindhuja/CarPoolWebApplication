using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;
using CarPool.AplicationData;
using CarPool.Data;

namespace CarPool.Services
{
    public class RideTakerServices:RideServices,IRideTakerServices
    {
        public GenericValidator genericValidator;

        public Repository repository;

        public RideTakerServices()
        {
            genericValidator = new GenericValidator();
            repository = new Repository();
        }
        public bool BookRide(Booking booking)
        {
            var ride = repository.FindItem<Ride>(r => r.RideId == booking.RideId);
            booking.BookingId = repository.Count<Booking>() + 1;
            booking.StartTime = ride.StartTime.AddSeconds( GetDurationBetweenPlaces(ride.Source, booking.Source));
            booking.EndTime= ride.StartTime.AddSeconds(GetDurationBetweenPlaces(booking.Source, booking.Destination));
            var placesList = new List<string>
            {
                ride.Source.ToLower()
            };
            placesList.AddRange(ride.ViaPlaces);
            placesList.Add(ride.Destination.ToLower());
            var root = placesList.GetRange(placesList.IndexOf(booking.Source.ToLower()) + 1, placesList.IndexOf(booking.Destination.ToLower()) - 1);
            booking.CostOfBooking = GetDistanceBetweenPlaces(booking.Source, booking.Destination,root) * ride.PricePerKilometer * booking.NumberSeatsSelected;
            booking.BookingDate = DateTime.Now;
            if (booking.NumberSeatsSelected <= ride.NoOfSeatsAvailable)
            {
                repository.Add<Booking>(booking);
                return true;         
            }
            return false;
        }

        public List<Booking> GetAllBookings(int userId)
        {
            return repository.FindData<Booking>(b => b.UserId == userId);
        }

        public  List<Ride> GetAllRideOffers(int userId)
        {
            return repository.FindData<Ride>(r => r.DateOfRide.Date >= DateTime.Now.Date && r.RideProviderId!=userId && r.NoOfSeatsAvailable>0);
        }

        public List<Ride> SearchRides(string pickupLocation, string dropLocation,int userId)
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
                        || ride.Destination.ToLower() == dropLocation)
                    {
                        availableRides.Add(ride);
                    }
                }   
            }
            return availableRides;
        }

        public Car GetCarDetails(string carNumber)
        {
            return repository.FindItem<Car>(c => c.CarNo == carNumber);
        }

    }
}
