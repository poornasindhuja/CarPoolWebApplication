using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;

namespace CarPool.Services
{
    class RideTakerServices
    {
        static int bookingId;
        public void showAllRides()
        {
            
        }
        public static void BookRide(string rideId,string userId)
        {
            Console.WriteLine("please Enter pickup location:");

            string pickupLocation = Console.ReadLine();

            Console.WriteLine("please Enter Drop location");

            string dropLocation = Console.ReadLine();

            Booking booking = new Booking((bookingId++).ToString(), rideId, pickupLocation, dropLocation, userId, DateTime.Now);

            RideServices.AddBookingToRide(rideId, booking);
        }
        public void ViewBookings()
        {

        }
    }
}
