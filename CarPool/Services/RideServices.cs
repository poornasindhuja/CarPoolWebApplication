using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;

namespace CarPool.Services
{
    public class RideServices
    {
        static List<Ride> Rides = new List<Ride>() {
            new Ride("1","1","09 TS 071998","Miyapur","Madhapur","19:00","20:00",3,new List<string>(){"kothaguda,kondapur,hitech,durgamcheruvu"},4),
            new Ride("2","1","10 TS 071930","Miyapur","Madhapur","07:00","08:25",3,new List<string>(){"kothaguda,kondapur,hitech,durgamcheruvu"},5),
            new Ride("3","2","01 TS 045398","Miyapur","Madhapur","12:00","12:50",3,new List<string>(){"kothaguda,kondapur,hitech,durgamcheruvu"},4)
        };

        static List<Car> cars = new List<Car>()
        {
            new Car("09 TS 071998","Maruthi",4,"NON-AC"),
            new Car("10 TS 071930","Suzuki",4,"AC"),
            new Car("01 TS 045398","Indica",4,"AC")
        };


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

                Car currentCar = cars.Find(c => c.CarNo == r.CarNumber);

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

        internal void AddCar(Car car)
        {
            cars.Add(car);
        }

        internal static void AddBookingToRide(string rideId,Booking booking)
        {
            Rides.Find(r => r.RideId == rideId).Bookings.Add(booking);
        }

        internal static void ApproveBooking(string rideId,string bookingId,bool val)
        {
            Rides.Find(r => r.RideId == rideId).Bookings.Find(b => b.bookingId == bookingId).status = val;
        }

        public List<Ride> getCurrentRides(string userId)
        {
            return Rides.FindAll(r => r.RideProviderId == userId);
        }

        public static void showBookings(string rideId)
        {
            int i = 1;

            foreach(Booking booking in Rides.Find(r => r.RideId == rideId).Bookings)
            {
                Console.WriteLine($"{i++}.");
                Console.WriteLine(booking.source);
                Console.WriteLine(booking.destination);
                Console.WriteLine($"Request From:{booking.userId}");
                Console.WriteLine("\n---------------------------------------------------------\n");
            }
        }

    }
}
