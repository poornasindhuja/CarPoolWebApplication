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
    class RideProviderFunctionalites
    {
        int choice, capacity,providerId;

        string carNo, carName;

        bool carType;

        IRideProviderServices rideProviderServices;

        IUserServices userServices;

        public RideProviderFunctionalites(int providerId)
        { 
            rideProviderServices= new RideProviderServices();

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
                    "2.Available Ride Offers\n" +
                    "3.Past Rides\n" +
                    "4.Go Back\n" +
                    "5.logout");

                int.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1:CreateRideOffer();
                        break;
                    case 2:AvailableRideOffers();
                        break;
                    case 3:ShowPastRideOffers();
                        break;
                    case 4:
                        repeat = false;
                        break;
                    case 5:
                        CarPoolMenu.DisplayMainMenu();
                        break;
                    default:Console.WriteLine("In correct option\n please choose a valid option");
                        break;
                }
            } while (repeat);
        }

        private void ShowPastRideOffers()
        {
            int i = 1;
            Console.WriteLine("---------------------------------------------------------------------------------------------------\n\n");
            var currentRides = rideProviderServices.GetPastRideOffers(providerId);
            if (currentRides.Count != 0)
            {
                foreach (Ride r in currentRides)
                {
                    Console.WriteLine($"{i++}.RideId:{r.RideId}\n From:{r.Source}\tTo:{r.Destination} \nStartTime:{r.StartTime.ToShortTimeString()}\tEndTime:{r.EndTime.ToShortTimeString()}");
                    int j = 1;
                    foreach (Booking booking in rideProviderServices.GetBookings(r.RideId))
                    {
                        Console.WriteLine($"\t{j++}.{booking.UserId}\t status:{booking.Status}");
                    }
                    if (rideProviderServices.GetBookings(r.RideId).Count == 0)
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
            var currentRides=rideProviderServices.GetAvailableRideOffers(providerId);

            int rideNo;

            if (currentRides.Count!=0)
            {
                Console.WriteLine("---------------------------------------------------------------------------------------------------\n\n");
                for(int i = 0; i < currentRides.Count; i++)
                {
                    Console.WriteLine($"{i+1}.Ridedate:{currentRides[i].DateOfRide.Date.ToShortDateString()}\n From:{currentRides[i].Source}\tTo:{currentRides[i].Destination} " +
                        $"\nStartTime:{currentRides[i].StartTime.ToShortTimeString()}\tEndTime:{currentRides[i].EndTime.ToShortTimeString()}\n" +
                        $"New Booking Requests:{rideProviderServices.GetNewBookingRequests(currentRides[i].RideId).Count}" +
                        $"\nNumber of seats yet to be filled:{currentRides[i].NoOfSeatsAvailable}");
                    Console.WriteLine("\n-----------------------------------------------------------------------------------------------------\n\n");
                }             
                do
                {
                    Console.WriteLine("please Enter an option from above to approve booking requests for that ride/enter 0 to go back");
                    int.TryParse(Console.ReadLine(), out rideNo);
                    if (rideNo == 0)
                    {
                        return;
                    }
                    else if(rideNo>currentRides.Count)
                    {
                        Console.WriteLine("Invalid Option");
                    }
                    else
                    {
                        break;
                    }
                } while (true);
                ApproveBookings(currentRides[rideNo - 1].RideId);
            }
            else
            {
                Console.WriteLine("No Ride Offers available\n press any key to go back");
                Console.ReadKey();
            }
        }

        private void ApproveBookings(int rideId)
        {
            var bookings = rideProviderServices.GetNewBookingRequests(rideId);
            int i = 1;
            foreach(Booking booking in bookings)
            {
                var requester = userServices.GetUser(booking.UserId);
                Console.WriteLine($"{i++}.\nRequested BY:{requester.UserName}\t" +
                    $"Phone Number:{requester.PhoneNumber}\nPick Up Location:{booking.Source}\tDrop Location:{booking.Destination}\n" +
                    $"Number of seats requested:{booking.NumberSeatsSelected}\tAmount you will get:Rs.{booking.CostOfBooking}");
            }
            if (bookings.Count == 0)
            {
                Console.WriteLine("Oops.! There are no Bookings for this ride\n press any key to go back");
                Console.ReadKey();
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("Please choose the booking you want to approve/reject:");

                    int.TryParse(Console.ReadLine(), out int bookingNo);
                    do
                    {
                        Console.WriteLine("Enter 1 to approve\nEnter 2 to reject");

                        int.TryParse(Console.ReadLine(), out choice);
                        if (choice == 1)
                        {
                            rideProviderServices.ApproveBooking(bookings[bookingNo-1].BookingId,BookingStatus.Approved);
                            Console.WriteLine("Request Approved");
                        }
                        else if (choice == 2)
                        {
                            rideProviderServices.ApproveBooking(bookings[bookingNo-1].BookingId, BookingStatus.Rejected);
                            Console.WriteLine("Request Rejected");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Option");
                            continue;
                        }
                        break;
                    } while (true);

                    Console.WriteLine("Enter 1 to continue approving");
                    Console.WriteLine("Enter 2 to Go Back");
                    int.TryParse(Console.ReadLine(), out choice);
                    if (choice == 2)
                    {
                        break;
                    }
                }
            }
                     
        }

        public void CreateRideOffer()
        {
            DateTime dateOfRide;

            int noOfSeats, index = 1;

            string time,source,destination;

            Console.Clear();
            try
            {
                if (rideProviderServices.IsCarLinked(providerId))
                {
                    Console.WriteLine("List Of Cars Used:");

                    var availableCars = rideProviderServices.GetCarsOfUser(providerId);

                    Console.WriteLine("Please Choose One of the following cars:");
                    for (int i = 0; i < availableCars.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}.{availableCars[i].CarNo}");
                    }
                    Console.WriteLine("Enter your choice/enter 0 to add new car ");
                    int.TryParse(Console.ReadLine(), out choice);
                    if (choice == 0)
                    {
                        AddCar(providerId);
                    }
                    else
                    {
                        carNo = availableCars[choice - 1].CarNo;
                        capacity = availableCars[choice - 1].Capacity;
                    }
                }
                else
                {
                    AddCar(providerId);
                }
                Console.WriteLine("please choose following places only");
                foreach (string place in CarPoolData.Places)
                {
                    Console.Write($"{place} ");
                }
                do
                {
                    Console.WriteLine("\nPlease enter Starting Location");
                    source = Console.ReadLine();
                } while (!RideDataValidations.IsValidPlace(source));
                do
                {
                    Console.WriteLine("Please enter start time (HH:MM) In 24 Hour Format");
                    time = Console.ReadLine();
                    time += ":00";
                } while (!RideDataValidations.IsValidTimeFormat(time));

                DateTime startTime = DateTime.ParseExact(time, "HH:mm:ss", CultureInfo.InvariantCulture);
                do
                {
                    Console.WriteLine("Please enter Destination Location");
                    destination = Console.ReadLine();
                } while (!RideDataValidations.IsValidPlace(destination));

                do
                {
                    Console.WriteLine("Please enter No of seats available");
                    int.TryParse(Console.ReadLine(),out noOfSeats);
                } while (noOfSeats > capacity - 1 || noOfSeats < 1);

                Console.WriteLine("Enter intermediate places(seperated by ',')");
                List<string> viaPlaces = new List<string>(Console.ReadLine().Split(','));

                for (int i = 0; i < viaPlaces.Count; i++)
                {
                    viaPlaces[i] = viaPlaces[i].ToLower();
                }

                do
                {
                    Console.WriteLine("Enter date of Journey(dd/mm/yyyy): ");
                    var date = Console.ReadLine().Split('/');
                    if (RideDataValidations.IsValidDateFormat(date))
                    {
                        dateOfRide = DateTime.Parse(date[1] + " / " + date[0] + " / " + date[2]);
                        break;
                    }
                    Console.WriteLine("In valid date format");
                } while (true);

                Console.WriteLine("Cost per Kilometer(Rupees.paise)");

                decimal amount = Convert.ToDecimal(Console.ReadLine());
                rideProviderServices.AddRide(providerId, carNo, source.ToLower(), destination.ToLower(), startTime, noOfSeats, viaPlaces, amount, dateOfRide);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            Console.WriteLine("Ride Added Sucessfully\nPress any key");
            Console.ReadKey();
        }

        public void AddCar(int providerId)
        {
          do
            {
                Console.WriteLine("Enter CarNo");
                carNo = Console.ReadLine();
            } while (carNo==null);

            do
            {
                Console.WriteLine("Enter Car Name");
                carName = Console.ReadLine();
            } while (carName.Length == 0);

            do
            {
                Console.WriteLine("Enter Capacity of car[4-8]");
                int.TryParse(Console.ReadLine(),out capacity);
            } while (capacity < 4 || capacity > 8);

            do
            {
                Console.WriteLine("select Car Type\n1.Ac\n2.Non-AC");
                int.TryParse(Console.ReadLine(), out choice);
            } while (choice < 1 || choice > 2);
           
            carType = choice == 1 ? true : false;
            rideProviderServices.AddCar(carNo, carName, capacity, carType,providerId);
        }
    }

}