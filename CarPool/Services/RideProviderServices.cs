using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;

namespace CarPool.Services
{
    class RideProviderServices
    {
        static int rideId = 4;

        RideServices rideServices = new RideServices();

        List<RideProvider> rideProviders = new List<RideProvider>();

        public void AddRide(string id)
        {
            
            Console.WriteLine("Enter CarNo");
       
            string carNo = Console.ReadLine();

            Console.WriteLine("Enter Car Name");

            string carName = Console.ReadLine();

            Console.WriteLine("Enter Capacity of car");

            int capacity = Convert.ToInt16(Console.ReadLine());

            Console.WriteLine("Car Type");

            string carType = Console.ReadLine();

            Console.WriteLine("Enter Starting Point");
            string source = Console.ReadLine();

            Console.WriteLine("Enter start time (HH:MM) In 24 Hour Format");
            string startTime = Console.ReadLine();
      
            Console.WriteLine("Enter End Point");
            string destination = Console.ReadLine();

            Console.WriteLine("Enter Reach time");
            string endTime = Console.ReadLine();

            Console.WriteLine("Enter No of seats available");
            int noOfSeats =Convert.ToInt16(Console.ReadLine());

            Console.WriteLine("Enter intermediate places(seperated by ',')");
            List<string> viaPlaces =new List<string>(Console.ReadLine().Split(','));

            Console.WriteLine("Cost per Kilometer(Rupees.paise");
            Decimal amount =Convert.ToDecimal(Console.ReadLine());

            rideServices.AddCar(new Car(carNo,carName,capacity,carType));
            rideServices.AddRide(new Ride(rideId++.ToString(),id,carNo, source, destination, startTime, endTime, noOfSeats, viaPlaces, amount));

            Console.WriteLine("Ride Added Sucessfully");

        }

        public void showCurrentRides(string userId)
        {
            int i=1;

            List<Ride> userRides=rideServices.getCurrentRides(userId);

            foreach(Ride r in userRides)
            {
                Console.WriteLine($"{i++}.RideId:{r.RideId}\n From:{r.Source}\tTo:{r.Destination} \nStartTime:{r.StartTime}\tEndTime:{r.EndTime}");
                foreach(Booking booking in r.Bookings)
                {
                    Console.WriteLine($"{booking.userId}\t status:{booking.status}");
                }
            }

            Console.WriteLine("Enter RideId in which you want to approve bookings");

            var rideId = Console.ReadLine();

            RideServices.showBookings(rideId);

            Console.WriteLine("Enter Booking Id you want to approve/reject:");

            var bookingId = Console.ReadLine();

            Console.WriteLine("Press 1 to approve\n press 2 to reject");

            int approve =Convert.ToInt16( Console.ReadLine());

            if (approve == 1)
            {
                RideServices.ApproveBooking(rideId, bookingId, true);
            }else if (approve == 2)
            {
                RideServices.ApproveBooking(rideId, bookingId, false);
            }
            else
            {
                Console.WriteLine("Invalid Option");
            }
        }
    }
}
