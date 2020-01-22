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

        void AddRide(int providerId, string carNo, string source, string destination, DateTime startTime, int noOfSeats, List<string> viaPlaces, decimal costPerKillometer, DateTime dateOfRide);

        List<Ride> GetAvailableRideOffers(int userId);

        void ApproveBooking(int bookingId, BookingStatus value);

        void AddCar(string carNo, string carName, int capacity, bool carType, int providerId);

        bool IsCarLinked(int providerId);

        List<Car> GetCarsOfUser(int userId);

        List<Booking> GetBookings(int rideId);

        List<Booking> GetNewBookingRequests(int rideId);

    }
}
