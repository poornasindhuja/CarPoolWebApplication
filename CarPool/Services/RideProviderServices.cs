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

//        List<RideProvider> rideProviders = new List<RideProvider>();

        public void AddRide(string id,string carNo,string source,string destination,DateTime startTime,DateTime endTime,int noOfSeats,List<string> viaPlaces,decimal costPerKillometer,DateTime dateOfRide)
        {
            
            //Console.WriteLine("Enter CarNo");
       
            //string carNo = Console.ReadLine();

            //Console.WriteLine("Enter Car Name");

            //string carName = Console.ReadLine();

            //Console.WriteLine("Enter Capacity of car");

            //int capacity = Convert.ToInt16(Console.ReadLine());

            //Console.WriteLine("Car Type");

            //string carType = Console.ReadLine();

            //Console.WriteLine("Enter Starting Point");
            //string source = Console.ReadLine();

            //Console.WriteLine("Enter start time (HH:MM) In 24 Hour Format");
            //string startTime = Console.ReadLine();
      
            //Console.WriteLine("Enter End Point");
            //string destination = Console.ReadLine();

            //Console.WriteLine("Enter Reach time");
            //string endTime = Console.ReadLine();

            //Console.WriteLine("Enter No of seats available");
            //int noOfSeats =Convert.ToInt16(Console.ReadLine());

            //Console.WriteLine("Enter intermediate places(seperated by ',')");
            //List<string> viaPlaces =new List<string>(Console.ReadLine().Split(','));

            //Console.WriteLine("Cost per Kilometer(Rupees.paise");
            //Decimal amount =Convert.ToDecimal(Console.ReadLine());
            rideServices.AddRide(new Ride(rideId++.ToString(),id,carNo, source, destination, startTime, endTime, noOfSeats, viaPlaces, costPerKillometer,dateOfRide));
        }

        public List<Ride> PastRides(string userId)
        {
            return rideServices.getPreviousRides(userId);
        }

        public List<Ride> CurrentRides(string userId)
        {
            return rideServices.getCurrentRides(userId);
        }

        public void ApproveBooking(string bookingId,bool value)
        {
            string rideId=null;

            Booking booking = RideServices.Bookings.Find(b => b.BookingId == bookingId);

            if (booking != null)
            {
                rideId = booking.RideId;
            }
            //string rideId = RideServices.Bookings.Find(b => b.BookingId == bookingId).RideId;
            rideServices.ApproveBooking(rideId, bookingId, value);
        }
       
        public void AddCar(string carNo, string carName, int capacity, bool carType, string providerId)
        {
            rideServices.AddCar(new Car(carNo, carName, capacity, carType, providerId));
            UserServices.CurrentUser.CarNo = carNo;
        }

        public bool IscarLinked(string providerId)
        {
            UserServices userServices = new UserServices();
            if(userServices.GetUser(providerId).CarNo!=null)
            {
                return true;
            }
            return false;
        }

        public List<Car> GetCars(string userId)
        {
            return RideServices.Cars.FindAll(c => c.OwnerId == userId);
        }

    }
}
