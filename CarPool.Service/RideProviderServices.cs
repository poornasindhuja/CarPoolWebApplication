using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using CarPool.AppData;
using CarPool.Models;

namespace CarPool.Services
{
    public class RideProviderServices:RideServices,IRideProviderServices
    {
        public bool AddRide(Ride ride)
        {
            bool isValidData = false;
            if(GenericValidator.Validate(ride,out ICollection<ValidationResult> results))
            {
                ride.RideId = CarPoolData.Rides.Count + 1;
                ride.EndTime = ride.StartTime.AddSeconds(GetDurationBetweenPlaces(ride.Source, ride.Destination));
                CarPoolData.Rides.Add(ride);
                isValidData = true;
            }
            return isValidData;
        }

        public List<Ride> GetPastRideOffers(int userId)
        {
            return CarPoolData.Rides.FindAll(r => r.RideProviderId == userId && r.DateOfRide.Date < DateTime.Now.Date);
        }

        public List<Ride> GetAvailableRideOffers(int userId)
        {
            return CarPoolData.Rides.FindAll(r => r.RideProviderId == userId && r.DateOfRide.Date>= DateTime.Now.Date);
        }

        public bool ApproveBooking(int bookingId,BookingStatus value)
        {           
            Booking booking = CarPoolData.Bookings.Find(b => b.BookingId == bookingId);

            Ride currentRide =CarPoolData.Rides.Find(r => r.RideId == booking.RideId);

            if (booking != null && currentRide.NoOfSeatsAvailable > 0)
            {
                CarPoolData.Bookings.Find(b => b.BookingId == bookingId).Status = value;
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
                CarPoolData.Cars.Add(car);
                return true;
            }
            return false;
        }

        public bool IsCarLinked(int providerId)
        {
            return CarPoolData.Cars.Count(c => c.OwnerId == providerId)!=0 ;
        }

        public List<Car> GetCarsOfUser(int userId)
        {
            return CarPoolData.Cars.FindAll(c => c.OwnerId == userId);
        }

        public List<Booking> GetBookingsForRide(int rideId)
        {
            return CarPoolData.Bookings.FindAll(b => b.RideId == rideId);
        }

        // returns all the bookings get by the user
        public List<Booking> GetAllBookings(int userId)
        {
            var ridesList = CarPoolData.Rides.FindAll(r => r.RideProviderId == userId).ConvertAll(r=>r.RideId);
            return CarPoolData.Bookings.FindAll(b => ridesList.Contains(b.RideId));
        }

        public List<Booking> GetNewBookingRequests(int rideId)
        {
            return CarPoolData.Bookings.FindAll(b => b.RideId == rideId && b.Status == BookingStatus.Pending);
        }
    }
}
