using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services
{
    public interface IRideProviderServices:IRideServices
    {
        List<Ride> GetPastRideOffers(int userId);

        void AddRide(Ride ride);

        List<Ride> GetAvailableRideOffers(int userId);

        void ApproveBooking(int bookingId, BookingStatus value);

        void AddCar(Car car);

        bool IsCarLinked(int providerId);

        List<Car> GetCarsOfUser(int userId);

        List<Booking> GetBookings(int rideId);

        List<Booking> GetNewBookingRequests(int rideId);

    }
}
