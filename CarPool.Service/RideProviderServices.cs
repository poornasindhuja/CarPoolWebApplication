using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using CarPool.Models;
using CarPool.AplicationData;
using CarPool.Data;

namespace CarPool.Services // bad usage of MapperHelper
{
    public class RideProviderServices:RideServices,IRideProviderServices
    {
        readonly Repository repository;

        public RideProviderServices()
        {
            repository = new Repository();
        }

        public bool AddRide(Ride ride)
        {
            bool isValidData = false;
            if(GenericValidator.Validate(ride,out ICollection<ValidationResult> results))
            {
                ride.EndTime = ride.StartTime.AddSeconds(GetDurationBetweenPlaces(ride.Source, ride.Destination));
                repository.Add<Data.Models.Ride>(MapperHelper.Map<Data.Models.Ride>(ride));
                isValidData = true;
            }
            return isValidData;
        }

        public List<Ride> GetPastRideOffers(int userId)
        {            
            return MapperHelper.MapCollection<Data.Models.Ride, Ride>(repository.FindAllItems<Data.Models.Ride>(r => r.RideProviderId == userId && r.DateOfRide.Date < DateTime.Now.Date)).ToList();
        }

        public List<Ride> GetAvailableRideOffers(int userId)
        {
            return MapperHelper.MapCollection<Data.Models.Ride,Ride>(repository.FindAllItems<Data.Models.Ride>(r => r.RideProviderId == userId && r.DateOfRide.Date >= DateTime.Now.Date)).ToList();
        }

        public bool ApproveBooking(int bookingId,BookingStatus value)
        {           
            Booking booking = repository.FindItem<Data.Models.Booking>(b => b.BookingId == bookingId).Map<Booking>();

            var currentRide = repository.FindItem<Data.Models.Ride>(r => r.RideId == booking.RideId);

            if (booking != null && currentRide.NoOfSeatsAvailable > 0)
            {
                var bookingModel=repository.FindItem<Data.Models.Booking>(b => b.BookingId == bookingId).Map<Booking>();
                bookingModel.Status = value;
                repository.Update<Data.Models.Booking>(MapperHelper.Map<Data.Models.Booking>(bookingModel));
                currentRide.NoOfSeatsAvailable = currentRide.NoOfSeatsAvailable - booking.NumberSeatsSelected;
                repository.Update<Data.Models.Ride>(currentRide);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddCar(Car car)
        {
            var errors = new List<string>();
            if (GenericValidator.Validate(car, out ICollection<ValidationResult> results))
            {
                repository.Add<Data.Models.Car>(MapperHelper.Map<Data.Models.Car>(car));
                return true;
            }
            return false;
        }

        public bool IsCarLinked(int providerId)
        {
            return repository.Count<Data.Models.Car>(c => c.OwnerId == providerId) !=0 ;
        }

        public List<Car> GetCarsOfUser(int userId)
        {
            return MapperHelper.MapCollection<Data.Models.Car,Car>(repository.FindAllItems<Data.Models.Car>(c => c.OwnerId == userId)).ToList();
        }

        public List<Booking> GetBookingsForRide(int rideId)
        {
            return MapperHelper.MapCollection<Data.Models.Booking,Booking>(repository.FindAllItems<Data.Models.Booking>(b => b.RideId == rideId)).ToList();
        }

        // Returns all the bookings get by the user
        public List<Booking> GetAllBookings(int userId)
        {
            var ridesList = repository.FindAllItems<Data.Models.Ride>(r => r.RideProviderId == userId).ConvertAll(r=>r.RideId);
            return MapperHelper.MapCollection<Data.Models.Booking,Booking>(repository.FindAllItems<Data.Models.Booking>(b => ridesList.Contains(b.RideId))).ToList();
        }

        public List<Booking> GetNewBookingRequests(int rideId)
        {
            return MapperHelper.MapCollection<Data.Models.Booking,Booking>(repository.FindAllItems<Data.Models.Booking>(b => b.RideId == rideId && b.Status == (short)BookingStatus.Pending)).ToList();
        }
    }
}
