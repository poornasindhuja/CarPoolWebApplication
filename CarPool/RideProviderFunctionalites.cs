﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.AppData;
using CarPool.Models;
using CarPool.Services;
using CarPool.Validations;

namespace CarPool
{
    public class RideProviderFunctionalites : Helper
    {
        public int Choice, capacity, providerId;

        public string carNo;

        IRideProviderServices rideProviderServices;

        IUserServices userServices;

        public RideProviderFunctionalites(int providerId)
        {
            rideProviderServices = new RideProviderServices();

            userServices = new UserServices();

            this.providerId = providerId;
        }

        public void ProviderOptions()
        {
            bool repeat = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Please Choose one of the following options\n" +
                    "1.Create New Ride Offer\n" +
                    "2.Created Ride Offers\n" +
                    "3.Past Rides\n" +
                    "4.Get All Booking requests" +
                    "5.Go Back\n" +
                    "6.logout");

                int.TryParse(Console.ReadLine(), out Choice);
                switch (Choice)
                {
                    case 1:
                        CreateRideOffer();
                        break;
                    case 2:
                        AvailableRideOffers();
                        break;
                    case 3:
                        ShowPastRideOffers();
                        break;
                    case 4:
                        DisplayAllBookingRequests();
                        break;
                    case 5:
                        repeat = false;
                        break;
                    case 6:
                        CarPoolMenu.DisplayMainMenu();
                        break;
                    default:
                        Console.WriteLine("In correct option\n please choose a valid option");
                        break;
                }
            } while (repeat);
        }

        private void DisplayAllBookingRequests()
        {
            rideProviderServices.GetAllBookings(providerId);
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
        }

        private void ShowPastRideOffers()
        {
            // It will display all ride offers provided by the current user which are completed.
            int i = 1;
            Console.WriteLine("---------------------------------------------------------------------------------------------------\n\n");
            var currentRides = rideProviderServices.GetPastRideOffers(providerId);
            if (currentRides.Count != 0)
            {
                foreach (Ride r in currentRides)
                {
                    Console.WriteLine($"{i++}.RideId:{r.RideId}\n From:{r.Source}\tTo:{r.Destination} \nStartTime:{r.StartTime.ToShortTimeString()}\tEndTime:{r.EndTime.ToShortTimeString()}");
                    int j = 1;
                    foreach (Booking booking in rideProviderServices.GetBookingsForRide(r.RideId))
                    {
                        Console.WriteLine($"\t{j++}.{booking.UserId}\t status:{booking.Status}");
                    }
                    if (rideProviderServices.GetBookingsForRide(r.RideId).Count == 0)
                    {
                        Console.WriteLine("Oops.! There are no Bookings for this ride");
                    }
                    Console.WriteLine("\n-----------------------------------------------------------------------------------------------------\n\n");
                }
            }
            else
            {
                Console.WriteLine("Oops! you have not provided any ride offer to show\n press any key to go back");
            }
            Console.ReadKey();
        }

        private void AvailableRideOffers()
        {
            var currentRides = rideProviderServices.GetAvailableRideOffers(providerId);
            int rideNo;
            Console.Clear();
            if (currentRides.Count == 0)
            {
                Console.WriteLine("No Ride Offers available\n press any key to go back");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------\n\n");
            for (int i = 0; i < currentRides.Count; i++)
            {
                Console.WriteLine($"RideNo:{i + 1}\tRidedate:{currentRides[i].DateOfRide.Date.ToShortDateString()}\n From:{currentRides[i].Source}\tTo:{currentRides[i].Destination} " +
                    $"\nStartTime:{currentRides[i].StartTime.ToShortTimeString()}\tEndTime:{currentRides[i].EndTime.ToShortTimeString()}\n" +
                    $"New Booking Requests:{rideProviderServices.GetNewBookingRequests(currentRides[i].RideId).Count}" +
                    $"\nNumber of seats yet to be filled:{currentRides[i].NoOfSeatsAvailable}" +
                    $"\n-----------------------------------------------------------------------------------------------------\n\n");
            }
            rideNo = GetIntegerInRange(minimumValue: 0, maximumValue: currentRides.Count,displayMessage:"please Enter an ride number from above to approve booking requests for that ride/enter 0 to go back",
                errorMessage:"Invalid ride number");
            if (rideNo == 0)
            {
                return;
            }
            else if (rideNo <= currentRides.Count)
            {
                ApproveBookings(currentRides[rideNo - 1].RideId);
            }
        }

        private void DisplayBookings(List<Booking> bookings)
        {
            int i = 1;
            Console.WriteLine("--------------------------------------------------------------------------------------------------------\n");
            foreach (Booking booking in bookings)
            {
                var requester = userServices.GetUser(booking.UserId);
                Console.WriteLine($"Booking number{i++}.\nRequested BY:{requester.UserName}\t" +
                    $"Phone Number:{requester.PhoneNumber}\nPick Up Location:{booking.Source}\tDrop Location:{booking.Destination}\n" +
                    $"Number of seats requested:{booking.NumberSeatsSelected}\tAmount you will get:Rs.{booking.CostOfBooking}" +
                    $"\n--------------------------------------------------------------------------------------------------------\n");      
            }
        }

        private void ApproveBookings(int rideId)
        {
            var bookings = rideProviderServices.GetNewBookingRequests(rideId);           
            if (bookings.Count == 0)
            {
                Console.WriteLine("Oops.! There are no Bookings for this ride\n press any key to go back");
                Console.ReadKey();
            }
            else
            {
                DisplayBookings(bookings);
                while (true)
                {
                    Console.WriteLine("Please choose the booking number you want to approve/reject:");

                    int.TryParse(Console.ReadLine(), out int bookingNo);
                    Choice = Convert.ToInt16(GetStringMatch("Enter 1 to approve\nEnter 2 to reject", "Invalid Option", @"^[12]"));
                    if (Choice == 1)
                    {
                        if(rideProviderServices.ApproveBooking(bookings[bookingNo - 1].BookingId, BookingStatus.Approved))
                        {
                            Console.WriteLine("Request Approved");
                        }
                        else
                        {
                            Console.WriteLine("Request can not be approved");
                        }             
                    }
                    else if (Choice == 2)
                    {
                        if(rideProviderServices.ApproveBooking(bookings[bookingNo - 1].BookingId, BookingStatus.Rejected))
                        {
                            Console.WriteLine("Request Rejected");
                        }
                        else
                        {
                            Console.WriteLine("Request can not be Rejected");
                        }
                    }
                    Console.WriteLine("Enter 1 to continue approving");
                    Console.WriteLine("Enter 2 to Go Back");
                    int.TryParse(Console.ReadLine(), out Choice);
                    if (Choice == 2)
                    {
                        break;
                    }
                }
            }

        }

        public void CreateRideOffer()
        {
            Ride ride = new Ride();

            string time;

            Console.Clear();
            // It will check if the user has provided ride offers previously,display list of cars he used previously and ask user to selsct one among them or new car.
            if (rideProviderServices.IsCarLinked(providerId))
            {
                Console.WriteLine("List Of Cars Used:");
                var availableCars = rideProviderServices.GetCarsOfUser(providerId);
                Console.WriteLine("Please Choose One of the following cars:");
                for (int i = 0; i < availableCars.Count; i++)
                {
                    Console.WriteLine($"{i + 1}.{availableCars[i].CarNo}");
                }
                do
                {
                    Console.WriteLine("Enter your Choice/enter * to add new car ");
                    string userChoice = Console.ReadLine();
                    int.TryParse(userChoice, out Choice);
                    if (userChoice == "*")
                    {
                        AddCar(providerId);
                        break;
                    }
                    else if(Choice>0&&Choice<=availableCars.Count)
                    {
                        carNo = availableCars[Choice - 1].CarNo;
                        capacity = availableCars[Choice - 1].Capacity;
                        break;
                    }
                } while (true);
            }
            else
            {
                if (!AddCar(providerId))
                {
                    Console.Write("Sorry! you can not create a ride offer.\nPress any key");
                    Console.ReadKey();
                    return;
                }
            }

            // Getting ride offer details from the user(like source,destination,starttime,end time..)
            var date = GetStringMatch("Enter date of Journey(dd/mm/yyyy):", "Invalid date format",Patterns.Date).Split('/');
            ride.DateOfRide = DateTime.Parse(date[1] + " / " + date[0] + " / " + date[2]);
            // Start Location
            Console.WriteLine("\nPlease select Starting Location");
            ride.Source = Enum.GetName(typeof(Places), GetUserChoiceInEnum<Places>());

            time = GetStringMatch("Please enter start time (HH:MM) In 24 Hour Format: ", "Invalid time", Patterns.Time);
            ride.StartTime = DateTime.ParseExact(time+":00", "HH:mm:ss", CultureInfo.InvariantCulture);

            Console.WriteLine("\nPlease select destination Location");
            ride.Destination = Enum.GetName(typeof(Places), GetUserChoiceInEnum<Places>());
            ride.NoOfSeatsAvailable = GetIntegerInRange("Please enter No of seats available", "Invalid Data",1,capacity-1);  
            // List of via points.
            Console.WriteLine("Enter Intermediate places seperated by ','(Please choose in the above listed places only):");
            var viaPlaces = new List<string>(Console.ReadLine().Split(',')).ConvertAll(place=>place.ToLower());
            foreach(string place in viaPlaces)
            {
                if (!CarPoolData.Places.Contains(place))
                {
                    Console.WriteLine($"{place}Could not be an intermediate place");
                    viaPlaces.Remove(place);
                } 
            }
            Console.WriteLine("Cost per Kilometer(Rupees.paise)");
            ride.PricePerKilometer =Convert.ToDecimal(GetStringMatch("Please Enter Cost per Kilometer(Rupees.paise)", "Invalid cost",Patterns.Amount));
            ride.CarNumber = carNo;
            ride.RideProviderId = providerId;
            ride.ViaPlaces = viaPlaces;
            rideProviderServices.AddRide(ride);
            Console.Write("Ride Added Sucessfully");
            Console.Write("Press any key");
            Console.ReadKey();
        }

        public bool AddCar(int providerId)
        {
            bool IsAcCar;

            string carName;

            carNo = GetStringMatch("Enter car number", "car number should not be empty",Patterns.CarNumber);
            carName = GetStringMatch("Enter the car name", "car name should not be empty", Patterns.Name);
            capacity =Convert.ToInt16(GetStringMatch("Enter Capacity of car[4-8]", "Invalid Capacity", Patterns.CarCapacity));
            var carTypeChoice = Convert.ToInt16(GetStringMatch("select Car Type\n1.Ac\n2.Non-AC", "Invalid cartype", @"^[1-2]"));
            IsAcCar = (carTypeChoice == 1);
            if(!rideProviderServices.AddCar(new Car(carNo, carName, capacity,IsAcCar, providerId)))
            { 
                Console.WriteLine("Oops! you cant add this car");
                return false;
            }
            return true;
        }

    }
}
