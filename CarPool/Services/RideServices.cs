using System;
using System.Collections.Generic;
using System.Globalization;
using CarPool.Models;

namespace CarPool.Services
{
    public class RideServices
    {
        static DateTime date=new DateTime(2020,2,1);

        static DateTime startTime = DateTime.ParseExact("12:00:00", "HH:mm:ss", CultureInfo.InvariantCulture);
        static DateTime endTime= DateTime.ParseExact("13:40:00", "HH:mm:ss", CultureInfo.InvariantCulture);
        public static List<Ride> Rides = new List<Ride>() {
            new Ride("1","1","09 TS 071998","miyapur","madhapur",startTime,endTime,3,new List<string>(){"kothaguda","kondapur","hitech","durgamcheruvu"},4,date),
            new Ride("2","1","10 TS 071930","miyapur","madhapur",startTime,endTime,3,new List<string>(){"kothaguda","kondapur","hitech","durgamcheruvu"},5,date),
            new Ride("3","2","01 TS 045398","miyapur","madhapur",startTime,endTime,3,new List<string>(){"kothaguda","kondapur","hitech","durgamcheruvu"},4,date)
        };

        public static List<Car> Cars = new List<Car>()
        {
            new Car("09 TS 071998","Maruthi",4,false,"1"),
            new Car("10 TS 071930","Suzuki",4,true,"1"),
            new Car("01 TS 045398","Indica",4,true,"2")
        };

        public static List<Booking> Bookings = new List<Booking>();

        public void AddRide(Ride ride)
        {
            Rides.Add(ride);
        }

        public void showRides()
        {
            int index = 1;

            foreach (Ride r in Rides)
            {
                Console.WriteLine($"{index++}.RideId:{r.RideId}\t\tRideProvide Name:{UserServices.users.Find(u => u.UserId == r.RideProviderId).UserName}");

                Car currentCar = Cars.Find(c => c.CarNo == r.CarNumber);

                Console.WriteLine($"Car Details:{ currentCar.CarName} Ac/NON-AC:{currentCar.CarType} {currentCar.CarNo} capacity:{currentCar.Capacity}");
                Console.WriteLine($"From: {r.Source}\t\t\tStarting Time:{r.StartTime}");
                Console.WriteLine($"To: {r.Destination}\t\t\t Reach By:{r.EndTime}");
                Console.WriteLine($"{r.NoOfSeatsAvailable} are available");
                foreach (string place in r.ViaPlaces)
                {
                    Console.Write(place + " ");
                }
                Console.WriteLine("\n--------------------------------------------------------------------------------------------------------\n");
            }
        }

        public Car GetCarDetails(string carNo) => Cars.Find(c => c.CarNo == carNo);

        public void AddCar(Car car)
        {
            Cars.Add(car);
        }

        internal bool IsvalidRideId(object rideId)
        {
            return Rides.Find(r => r.RideId == rideId) != null ? true : false;
        }

        public List<Ride> getPreviousRides(string userId) => Rides.FindAll(r => r.RideProviderId == userId && r.DateOfRide < DateTime.Now);

        //public static void AddBookingToRide(string rideId, Booking booking) => Rides.Find(r => r.RideId == rideId).Bookings.Add(booking);

        public void ApproveBooking(string rideId,string bookingId,bool val)
        {
            Booking booking = Bookings.Find(b => b.BookingId == bookingId);

            Ride currentRide = Rides.Find(r => r.RideId == rideId);

            if (booking != null && currentRide.NoOfSeatsAvailable>0)
            {
                booking.Status = val;
                booking.DoesProviderViewed = true;
                currentRide.Bookings.Add(bookingId);
                currentRide.NoOfSeatsAvailable = currentRide.NoOfSeatsAvailable - 1;
            }  
        }

        public List<Ride> getCurrentRides(string userId)
        {
            return Rides.FindAll(r => r.RideProviderId == userId && r.DateOfRide>=DateTime.Now);
        }

        //public static void showBookings(string rideId)
        //{
        //    int i = 1;

        //    foreach(Booking booking in Rides.Find(r => r.RideId == rideId).Bookings)
        //    {
        //        Console.WriteLine($"{i++}.");
        //        Console.WriteLine(booking.Source);
        //        Console.WriteLine(booking.Destination);
        //        Console.WriteLine($"Request From:{booking.UserId}");
        //        Console.WriteLine("\n---------------------------------------------------------\n");
        //    }
        //}

        public List<Booking> GetBookings(string rideId)
        {
            return Bookings.FindAll(b => b.RideId == rideId);
        }

        public List<Booking> GetNewBookingRequests(string rideId)
        {
            return Bookings.FindAll(b => b.RideId == rideId && b.DoesProviderViewed == false);
        }
    }
}
