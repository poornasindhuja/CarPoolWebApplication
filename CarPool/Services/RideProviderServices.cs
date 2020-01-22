using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.AppData;
using CarPool.Models;

namespace CarPool.Services
{
    class RideProviderServices:RideServices,IRideProviderServices
    {

        public void AddRide(int providerId,string carNo,string source,string destination,DateTime startTime,int noOfSeats,List<string> viaPlaces,decimal costPerKillometer,DateTime dateOfRide)
        {
            DateTime endTime = startTime.AddSeconds(GetDurationBetweenPlaces(source, destination));
            CarPoolData.Rides.Add(new Ride((CarPoolData.Rides.Count+1),providerId,carNo, source, destination, startTime, endTime, noOfSeats, viaPlaces, costPerKillometer,dateOfRide));
        }

        public List<Ride> GetPastRideOffers(int userId)
        {
            return CarPoolData.Rides.FindAll(r => r.RideProviderId == userId && r.DateOfRide.Date < DateTime.Now.Date);
        }

        public List<Ride> GetAvailableRideOffers(int userId)
        {
            return CarPoolData.Rides.FindAll(r => r.RideProviderId == userId && r.DateOfRide.Date>= DateTime.Now.Date);
        }

        public void ApproveBooking(int bookingId,BookingStatus value)
        {
            
            Booking booking = CarPoolData.Bookings.Find(b => b.BookingId == bookingId);

            Ride currentRide =CarPoolData.Rides.Find(r => r.RideId == booking.RideId);

            if (booking != null && currentRide.NoOfSeatsAvailable > 0)
            {
                CarPoolData.Bookings.Find(b => b.BookingId == bookingId).Status = value;
                currentRide.NoOfSeatsAvailable = currentRide.NoOfSeatsAvailable - booking.NumberSeatsSelected;
            }
        }
       
        public void AddCar(string carNo, string carName, int capacity, bool carType, int providerId)
        {
            CarPoolData.Cars.Add(new Car(carNo, carName, capacity, carType, providerId));
        }

        public bool IsCarLinked(int providerId)
        {
            return CarPoolData.Cars.FindAll(c => c.OwnerId == providerId).Count!=0 ? true : false;
        }

        public List<Car> GetCarsOfUser(int userId)
        {
            return CarPoolData.Cars.FindAll(c => c.OwnerId == userId);
        }

        public List<Booking> GetBookings(int rideId)
        {
            return CarPoolData.Bookings.FindAll(b => b.RideId == rideId);
        }

        public List<Booking> GetNewBookingRequests(int rideId)
        {
            return CarPoolData.Bookings.FindAll(b => b.RideId == rideId && b.Status == BookingStatus.Pending);
        }
    }
}
