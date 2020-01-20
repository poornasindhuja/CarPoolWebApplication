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

        RideServices rideServices;

        UserServices userServices;

        public RideTakerActions()
        {
            rideTakerServices = new RideTakerServices();

            rideServices = new RideServices();

            userServices = new UserServices();
        }

        public void RideTakerOptions()
        {
            bool repeat = true;

            while (repeat)
            {
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
                    case 3:viewBookings();
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

        private void viewBookings()
        {
            List<Booking> bookings=rideTakerServices.GetAllBookings();
            for(int i = 0; i < bookings.Count; i++)
            {
                Console.WriteLine($"\n-------------------------------------------------------------------------------------------------------------\n" +
                    $"{i}.Booking Date:{bookings[i].BookingDate.ToShortDateString()}\nFrom:{bookings[i].Source}\t\tTo:{bookings[i].Destination}\nJourney Time:{bookings[i].StartTime}-{bookings[i].EndTime}");
                //$"Status:{(bookings[i].Status?"Approved":"NotApproved")}");
                if (bookings[i].DoesProviderViewed)
                {
                    Console.WriteLine($"Status:{(bookings[i].Status ? "Approved" : "NotApproved")}");
                }
                else
                {
                    Console.WriteLine($"Status:Pending");
                }
            }
            
            Console.WriteLine($"-------------------------------------------------------------------------------------------------------------");
            
        }

        private void BookRide()
        {
            string rideId;

            Console.Clear();

            while (true)
            {
                Console.WriteLine("please Enter pickup location:");

                string pickupLocation = Console.ReadLine();

                Console.WriteLine("please Enter Drop location");

                string dropLocation = Console.ReadLine();

                List<Ride> availableRides = rideTakerServices.searchRides(pickupLocation.ToLower(), dropLocation.ToLower());

                if (availableRides.Count == 0)
                {
                    Console.WriteLine("No rides Available to show");
                }
                else
                {
                    DisplayRideOffers(availableRides);
                    do
                    {
                        Console.WriteLine("Select a ride by entering rideId:");
                        rideId = Console.ReadLine();
                    } while (rideServices.IsvalidRideId(rideId));

                    rideTakerServices.BookRide(rideId, pickupLocation, dropLocation);
                    Console.Clear();
                    Console.WriteLine("Request Sent sucessfully");
                }

                Console.WriteLine("Enter 1 to Continue Booking\n Enter 2 to Go Back");
                int.TryParse(Console.ReadLine(), out choice);
                if (choice == 2)
                {
                    break;
                }
            }

            
        }

        public void DisplayRideOffers(List<Ride> rideOffers)
        {
            int index = 1;

            foreach (Ride r in rideOffers)
            {
                Console.WriteLine($"{index++}.RideId:{r.RideId}\t\tRideProvide Name:{UserServices.users.Find(u => u.UserId == r.RideProviderId).UserName}" +
                    $"\nFrom: { r.Source}\t\tTo: {r.Destination}\nVia:");
                foreach (string place in r.ViaPlaces)
                {
                    Console.Write(place + " ");
                }
                Car currentCar = rideServices.GetCarDetails(r.CarNumber);
                Console.WriteLine($"\nStarting Time:{ r.StartTime.ToShortTimeString()}\t\t\t Reach By:{r.EndTime.ToShortTimeString()}\nSeats Available={r.NoOfSeatsAvailable}\n Journey Date:{r.DateOfRide.ToShortDateString()}");
                Console.WriteLine($"Car Details\nCarName:{ currentCar.CarName}\t Ac/NON-AC:{(currentCar.CarType?"Ac":"Non-Ac")}\nCarNo:{currentCar.CarNo}\t capacity:{currentCar.Capacity}");                
                Console.WriteLine("\n--------------------------------------------------------------------------------------------------------\n");   
            }
            if (rideOffers == null)
            {
                Console.WriteLine("No rides Available to show");
            }
        }

        private void DisplayAllRideOffers()
        {
            Console.Clear();

            List<Ride> rideOffers = rideTakerServices.getAllRideOffers();

            DisplayRideOffers(rideOffers);
            Console.WriteLine("Press any key to GoBack");
            Console.ReadKey();
        }
    }
}
