using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using CarPool.Models;
using CarPool.AplicationData;
using CarPool.Data;

namespace CarPool.Services
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
                ride.RideId = repository.GetAllData<Ride>().Count() + 1;
                ride.EndTime = ride.StartTime.AddSeconds(GetDurationBetweenPlaces(ride.Source, ride.Destination));
                repository.Add<Ride>(ride);
                isValidData = true;
            }
            return isValidData;
        }

        public List<Ride> GetPastRideOffers(int userId)
        {
            return repository.FindData<Ride>(r => r.RideProviderId == userId && r.DateOfRide.Date < DateTime.Now.Date);
        }

        public List<Ride> GetAvailableRideOffers(int userId)
        {
            // Implement  (&& r.DateOfRide>= DateTime.Now)
            return repository.FindData<Ride>(r => r.RideProviderId == userId);
        }

        public bool ApproveBooking(int bookingId,BookingStatus value)
        {           
            Booking booking = repository.FindItem<Booking>(b => b.BookingId == bookingId);

            Ride currentRide = repository.FindItem<Ride>(r => r.RideId == booking.RideId);

            if (booking != null && currentRide.NoOfSeatsAvailable > 0)
            {
                //..........?
                //CarPoolData.Bookings.Find(b => b.BookingId == bookingId).Status = value;
                currentRide.NoOfSeatsAvailable = currentRide.NoOfSeatsAvailable - booking.NumberSeatsSelected;
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
                repository.Add<Car>(car);
                return true;
            }
            return false;
        }

        public bool IsCarLinked(int providerId)
        {
            return repository.FindData<Car>(c => c.OwnerId == providerId).Count()!=0 ;
        }

        public List<Car> GetCarsOfUser(int userId)
        {
            return repository.FindData<Car>(c => c.OwnerId == userId);
        }

        public List<Booking> GetBookingsForRide(int rideId)
        {
            return repository.FindData<Booking>(b => b.RideId == rideId);
        }

        // returns all the bookings get by the user
        public List<Booking> GetAllBookings(int userId)
        {
            var ridesList = repository.FindData<Ride>(r => r.RideProviderId == userId).ConvertAll(r=>r.RideId);
            return repository.FindData<Booking>(b => ridesList.Contains(b.RideId));
        }

        public List<Booking> GetNewBookingRequests(int rideId)
        {
            return repository.FindData<Booking>(b => b.RideId == rideId && b.Status == BookingStatus.Pending);
        }
    }
}
