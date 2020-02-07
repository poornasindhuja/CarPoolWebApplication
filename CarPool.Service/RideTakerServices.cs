using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;
using CarPool.AplicationData;
using CarPool.Data;
using AutoMapper;
using CarPool.Service;

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
            repository = new Repository();//is there need of initializing twice?
        }
        public bool BookRide(Booking booking)
        {
            var ride = repository.FindItem<Data.Models.Ride>(r => r.RideId == booking.RideId).Map<Ride>();
            // booking.BookingId = repository.Count<Booking>() + 1; remove unwanted code
            booking.StartTime = ride.StartTime.AddSeconds( GetDurationBetweenPlaces(ride.Source, booking.Source));
            booking.EndTime= ride.StartTime.AddSeconds(GetDurationBetweenPlaces(booking.Source, booking.Destination));
            var placesList = new List<string>
            {
                ride.Source.ToLower()
            };
            placesList.AddRange(ride.ViaPlaces);
            placesList.Add(ride.Destination);
            var root = placesList.GetRange(placesList.IndexOf(booking.Source) + 1, placesList.IndexOf(booking.Destination) - 1);
            booking.CostOfBooking = GetDistanceBetweenPlaces(booking.Source, booking.Destination,root) * ride.PricePerKilometer * booking.NumberSeatsSelected;
            booking.BookingDate = DateTime.Now;
            if (booking.NumberSeatsSelected <= ride.NoOfSeatsAvailable)
            {
                repository.Add<Data.Models.Booking>(MapperHelper.Map<Data.Models.Booking>(booking));
                return true;         
            }
            return false;
        }

        public List<Booking> GetAllBookings(int userId)
        {
            return MapperHelper.MapCollection<Data.Models.Booking,Booking>(repository.FindAllItems<Data.Models.Booking>(b => b.UserId == userId)).ToList();
        }

        public  List<Ride> GetAllRideOffers(int userId)
        {
            return MapperHelper.MapCollection<Data.Models.Ride,Ride>(repository.FindAllItems<Data.Models.Ride>(r => r.DateOfRide.Date >= DateTime.Now.Date && r.RideProviderId != userId && r.NoOfSeatsAvailable > 0)).ToList();
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
            return repository.FindItem<Data.Models.Car>(c => c.CarNo == carNumber).Map<Car>();
        }

    }
}
