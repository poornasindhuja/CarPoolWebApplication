using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;
using CarPool.Services;

namespace CarPool
{
    public class RideTakerFunctionalities
    {
        int Choice,userId;

        IRideTakerServices rideTakerServices;

        IUserServices userServices;

        public RideTakerFunctionalities(int userId)
        {
            rideTakerServices = new RideTakerServices();

            userServices = new UserServices();

            this.userId = userId;
        }

        public void RideTakerOptions()
        {
            // Displays the options for ridetaker
            bool repeat = true;

            while (repeat)
            {
                Console.Clear();
                Console.WriteLine("Please Choose one of the following options" +
                    "1.view Ride Offers" +
                    "2.Book a Ride" +
                    "3.View Bookings" +
                    "4.Go Back" +
                    "5.Logout");
                int.TryParse(Console.ReadLine(), out Choice);

                switch (Choice)
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
                    case 5:
                        CarPoolMenu.DisplayMainMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid option\n please choose a valid option");
                        break;
                }

            }

        }

        private void ViewBookings()
        {
            var bookings=rideTakerServices.GetAllBookings(userId);
            if (bookings.Count == 0)
            {
                Console.WriteLine("Oops! there are no booking requests to show");
            }
            for(int i = 0; i < bookings.Count; i++)
            {
                Console.WriteLine($"\n-------------------------------------------------------------------------------------------------------------\n" +
                    $"{i}.Booking Date:{bookings[i].BookingDate.ToShortDateString()}\nFrom:{bookings[i].Source}\t\tTo:{bookings[i].Destination}\n" +
                    $"Journey Time:{bookings[i].StartTime.ToShortTimeString()}-{bookings[i].EndTime.ToShortTimeString()}");
                //$"Status:{(bookings[i].Status?"Approved":"NotApproved")}");              
                    Console.WriteLine($"Status:{Enum.GetName(typeof(BookingStatus),bookings[i].Status)}\tCost:{bookings[i].CostOfBooking}"); 
            }       
            Console.WriteLine($"-------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
        }

        private void BookRide()
        {
            int noOfSeats;

            Helper helper = new Helper();

            Console.Clear();
            Console.WriteLine("please Enter pick up location:");

            string pickupLocation =Enum.GetName(typeof(Places),helper.GetUserChoiceInEnum<Places>());

            Console.WriteLine("please Enter Drop location");

            string dropLocation = Enum.GetName(typeof(Places), helper.GetUserChoiceInEnum<Places>());

            var availableRides = rideTakerServices.SearchRides(pickupLocation.ToLower(), dropLocation.ToLower(),userId);

            if (availableRides.Count == 0)
            {
                Console.WriteLine("No rides Available to show\npress any key to continue");
                Console.ReadKey();
            }
            else
            {
                DisplayRideOffers(availableRides, pickupLocation, dropLocation);
                do
                {
                    Console.WriteLine("Please select your option/Enter '0' to go back:");
                    int.TryParse(Console.ReadLine(), out Choice);
                    if (Choice == 0)
                    {
                        return;
                    }
                    do
                    {
                        Console.WriteLine("Enter number of seats you want to book");
                        int.TryParse(Console.ReadLine(), out noOfSeats);
                    } while (noOfSeats > availableRides[Choice - 1].NoOfSeatsAvailable || noOfSeats < 1);
                } while (!rideTakerServices.IsValidRideId(availableRides.ElementAt(Choice-1).RideId));

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Enter 1 to confirm Booking.\nEnter 2 to cancel Booking");
                    int.TryParse(Console.ReadLine(), out Choice);
                    if (Choice == 1)
                    {
                        if (rideTakerServices.BookRide(new Booking(availableRides[Choice - 1].RideId, pickupLocation, dropLocation, noOfSeats, userId)))
                        {
                            Console.Clear();
                            Console.WriteLine("Request Sent sucessfully");
                        }
                        else
                        {
                            Console.WriteLine("Request can not be sent");
                        }
                        break;
                    }
                    else if (Choice == 2)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Choice");
                    }
                }
            }           
        }

        private void DisplayRideOffers(IList<Ride> rideOffers,string source,string destination)
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

                var currentCar = rideTakerServices.GetCarDetails(r.CarNumber);

                Console.WriteLine($"\nStarting Time:{ r.StartTime.ToShortTimeString()}\t\t\t Reach By:{r.EndTime.ToShortTimeString()}\nSeats Available={r.NoOfSeatsAvailable}\n " +
                    $"Journey Date:{r.DateOfRide.ToShortDateString()}Car Details\nCarName:{ currentCar.CarName}\t Ac/NON-AC:{(currentCar.CarType?"Ac":"Non-Ac")}\n" +
                    $"CarNo:{currentCar.CarNo}\t capacity:{currentCar.Capacity}");

                costOfRide = rideTakerServices.GetDistanceBetweenPlaces(source,destination) * r.PricePerKilometer;
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

            var rideOffers = rideTakerServices.GetAllRideOffers(userId);
            if (rideOffers.Count == 0)
            {
                Console.WriteLine("No rides Available to show");
            }
            else
            {
                Console.WriteLine("\n--------------------------------------------------------------------------------------------------------\n");
                foreach (Ride r in rideOffers)
                {
                    Console.WriteLine($"Ride Number{index++}\t\tRideProvide Name:{userServices.GetUser(r.RideProviderId).UserName}" +
                        $"\nFrom: { r.Source}\t\tTo: {r.Destination}\nVia:");
                    foreach (string place in r.ViaPlaces)
                    {
                        Console.Write(place + " ");
                    }

                    var currentCar = rideTakerServices.GetCarDetails(r.CarNumber);

                    Console.WriteLine($"\nStarting Time:{ r.StartTime.ToShortTimeString()}\t\t\t Reach By:{r.EndTime.ToShortTimeString()}\n" +
                        $"Seats Available={r.NoOfSeatsAvailable}\nJourney Date:{r.DateOfRide.ToShortDateString()}Car Details\nCarName:{ currentCar.CarName}" +
                        $"\t Ac/NON-AC:{(currentCar.CarType ? "Ac" : "Non-Ac")}\nCarNo:{currentCar.CarNo}\t capacity:{currentCar.Capacity}");
                    costOfRide = rideTakerServices.GetDistanceBetweenPlaces(r.Source, r.Destination) * r.PricePerKilometer;
                    Console.WriteLine($"Cost:{costOfRide}");
                    Console.WriteLine("\n--------------------------------------------------------------------------------------------------------\n");
                }
            } 
            Console.WriteLine("Press any key to GoBack");
            Console.ReadKey();
        }
    }

    
}
