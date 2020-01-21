using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.AppData;
using CarPool.Models;

namespace CarPool.Services
{
    class RideProviderServices
    {

        public void AddRide(int providerId,string carNo,string source,string destination,DateTime startTime,DateTime endTime,int noOfSeats,List<string> viaPlaces,decimal costPerKillometer,DateTime dateOfRide)
        {
            Database.Rides.Add(new Ride((Database.Rides.Count+1),providerId,carNo, source, destination, startTime, endTime, noOfSeats, viaPlaces, costPerKillometer,dateOfRide));
        }

        public List<Ride> GetPastRideOffers(int userId)
        {
            //return rideServices.getPreviousRides(userId);
            return Database.Rides.FindAll(r => r.RideProviderId == userId && r.DateOfRide < DateTime.Now);
        }

        public List<Ride> GetAvailableRideOffers(int userId)
        {
            return Database.Rides.FindAll(r => r.RideProviderId == userId && r.DateOfRide >= DateTime.Now);
        }

        public void ApproveBooking(int bookingId,BookingStatus value)
        {
            
            Booking booking = Database.Bookings.Find(b => b.BookingId == bookingId);

            Ride currentRide =Database.Rides.Find(r => r.RideId == booking.RideId);

            if (booking != null && currentRide.NoOfSeatsAvailable > 0)
            {
                Database.Bookings.Find(b => b.BookingId == bookingId).Status = value;
                currentRide.NoOfSeatsAvailable = currentRide.NoOfSeatsAvailable - 1;
            }
        }
       
        public void AddCar(string carNo, string carName, int capacity, bool carType, int providerId)
        {
            Database.Cars.Add(new Car(carNo, carName, capacity, carType, providerId));
            UserServices.CurrentUser.CarNo = carNo;
        }

        public bool IsCarLinked(int providerId)
        {
            return Database.Cars.FindAll(c => c.OwnerId == providerId).Count!=0 ? true : false;
        }

        public List<Car> GetCarsOfUser(int userId)
        {
            return Database.Cars.FindAll(c => c.OwnerId == userId);
        }

        public List<Booking> GetBookings(int rideId)
        {
            return Database.Bookings.FindAll(b => b.RideId == rideId);
        }

        public List<Booking> GetNewBookingRequests(int rideId)
        {
            return Database.Bookings.FindAll(b => b.RideId == rideId && b.Status == BookingStatus.Pending);
        }
    }
}
