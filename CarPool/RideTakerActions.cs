using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;
using CarPool.Services;

namespace CarPool
{
    class RideTakerActions
    {
        int choice;

        RideTakerServices rideTakerServices;

        UserServices userServices;

        RideServices rideServices;

        public RideTakerActions()
        {
            rideTakerServices = new RideTakerServices();

            userServices = new UserServices();

            rideServices = new RideServices();
        }

        public void RideTakerOptions()
        {
            bool repeat = true;

            while (repeat)
            {
                Console.Clear();
                Console.WriteLine("Please Choose one of the following options");
                Console.WriteLine("1.view Ride Offers");
                Console.WriteLine("2.Book a Ride");
                Console.WriteLine("3.View Bookings");
                Console.WriteLine("4.Go Back");
                Console.WriteLine("5.Logout");
                int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        DisplayAllRideOffers();
                        break;
                    case 2:
                        BookRide();
                        break;
                    case 3:ViewBookings();
                        break;
                    case 4:
                        repeat = false;
                        break;
                    case 5: return;
                    default:
                        Console.WriteLine("Invalid option\n please choose a valid option");
                        break;
                }

            }

        }

        private void ViewBookings()
        {
            List<Booking> bookings=rideTakerServices.GetAllBookings();
            for(int i = 0; i < bookings.Count; i++)
            {
                Console.WriteLine($"\n-------------------------------------------------------------------------------------------------------------\n" +
                    $"{i}.Booking Date:{bookings[i].BookingDate.ToShortDateString()}\nFrom:{bookings[i].Source}\t\tTo:{bookings[i].Destination}\n" +
                    $"Journey Time:{bookings[i].StartTime.ToShortTimeString()}-{bookings[i].EndTime.ToShortTimeString()}");
                //$"Status:{(bookings[i].Status?"Approved":"NotApproved")}");              
                    Console.WriteLine($"Status:{Enum.GetName(typeof(BookingStatus),bookings[i].Status)}\tCost:{bookings[i]}"); 
            }
            
            Console.WriteLine($"-------------------------------------------------------------------------------------------------------------");
            
        }

        private void BookRide()
        {
            int rideId;

            Console.Clear();

            while (true)
            {
                Console.WriteLine("please Enter pick up location:");

                string pickupLocation = Console.ReadLine();

                Console.WriteLine("please Enter Drop location");

                string dropLocation = Console.ReadLine();

                List<Ride> availableRides = rideTakerServices.SearchRides(pickupLocation.ToLower(), dropLocation.ToLower());

                if (availableRides.Count == 0)
                {
                    Console.WriteLine("No rides Available to show");
                }
                else
                {
                    DisplayRideOffers(availableRides,pickupLocation,dropLocation);
                    do
                    {
                        Console.WriteLine("Select a ride by entering rideId/Enter '0' to go back:");
                        int.TryParse( Console.ReadLine(),out rideId);
                        if (rideId == 0)
                        {
                            return;
                        }
                    } while (rideServices.IsvalidRideId(rideId));

                    rideTakerServices.BookRide(rideId, pickupLocation, dropLocation);
                    Console.Clear();
                    Console.WriteLine("Request Sent sucessfully");
                }

                Console.WriteLine("Enter 1 to continue booking\n Enter 2 to Go Back");
                int.TryParse(Console.ReadLine(), out choice);
                if (choice == 2)
                {
                    break;
                }
            }

            
        }

        public void DisplayRideOffers(List<Ride> rideOffers,string source,string destination)
        {
            int index = 1;

            decimal costOfRide;
            
            Console.WriteLine("\n--------------------------------------------------------------------------------------------------------\n");
            foreach (Ride r in rideOffers)
            {
                Console.WriteLine($"{index++}.RideId:{r.RideId}\t\tRideProvide Name:{userServices.GetUser(r.RideProviderId).UserName}" +
                    $"\nFrom: { r.Source}\t\tTo: {r.Destination}\nVia:");
                foreach (string place in r.ViaPlaces)
                {
                    Console.Write(place + " ");
                }

                Car currentCar = rideTakerServices.GetCarDetails(r.CarNumber);

                Console.WriteLine($"\nStarting Time:{ r.StartTime.ToShortTimeString()}\t\t\t Reach By:{r.EndTime.ToShortTimeString()}\nSeats Available={r.NoOfSeatsAvailable}\n " +
                    $"Journey Date:{r.DateOfRide.ToShortDateString()}Car Details\nCarName:{ currentCar.CarName}\t Ac/NON-AC:{(currentCar.CarType?"Ac":"Non-Ac")}\n" +
                    $"CarNo:{currentCar.CarNo}\t capacity:{currentCar.Capacity}");

                costOfRide = rideServices.GetDistanceBetweenPlaces(source,destination) * r.PricePerKilometer;
                Console.WriteLine($"Cost:{costOfRide}");
                Console.WriteLine("\n--------------------------------------------------------------------------------------------------------\n");   
            }
            if (rideOffers == null)
            {
                Console.WriteLine("No rides Available to show");
            }
        }

        private void DisplayAllRideOffers()
        {
            int index = 1;

            decimal costOfRide;

            Console.Clear();

            List<Ride> rideOffers = rideTakerServices.GetAllRideOffers();

            Console.WriteLine("\n--------------------------------------------------------------------------------------------------------\n");
            foreach (Ride r in rideOffers)
            {
                Console.WriteLine($"{index++}.RideId:{r.RideId}\t\tRideProvide Name:{userServices.GetUser(r.RideProviderId).UserName}" +
                    $"\nFrom: { r.Source}\t\tTo: {r.Destination}\nVia:");
                foreach (string place in r.ViaPlaces)
                {
                    Console.Write(place + " ");
                }

                Car currentCar = rideTakerServices.GetCarDetails(r.CarNumber);

                Console.WriteLine($"\nStarting Time:{ r.StartTime.ToShortTimeString()}\t\t\t Reach By:{r.EndTime.ToShortTimeString()}\nSeats Available={r.NoOfSeatsAvailable}\n " +
                    $"Journey Date:{r.DateOfRide.ToShortDateString()}Car Details\nCarName:{ currentCar.CarName}\t Ac/NON-AC:{(currentCar.CarType ? "Ac" : "Non-Ac")}\n" +
                    $"CarNo:{currentCar.CarNo}\t capacity:{currentCar.Capacity}");

                costOfRide = rideServices.GetDistanceBetweenPlaces(r.Source, r.Destination) * r.PricePerKilometer;
                Console.WriteLine($"Cost:{costOfRide}");
                Console.WriteLine("\n--------------------------------------------------------------------------------------------------------\n");
            }
            if (rideOffers == null)
            {
                Console.WriteLine("No rides Available to show");
            }
            Console.WriteLine("Press any key to GoBack");
            Console.ReadKey();
        }
    }

    
}
