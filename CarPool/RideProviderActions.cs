using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;
using CarPool.Services;
using CarPool.Validations;

namespace CarPool
{
    class RideProviderActions
    {
        int choice, capacity;

        string carNo, carName;

        bool carType;

        RideProviderServices rideProviderServices;

        UserServices userServices;

        RideServices rideServices;

        public RideProviderActions()
        {
            userServices= new UserServices();

            rideProviderServices= new RideProviderServices();

            rideServices = new RideServices();
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
                    case 5:userServices.Logout();
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
            List<Ride> currentRides = rideProviderServices.GetPastRideOffers(UserServices.CurrentUser.UserId);
            foreach (Ride r in currentRides)
            {
                Console.WriteLine($"{i++}.RideId:{r.RideId}\n From:{r.Source}\tTo:{r.Destination} \nStartTime:{r.StartTime.ToShortTimeString()}\tEndTime:{r.EndTime.ToShortTimeString()}");
                int j = 1;
                foreach (Booking booking in rideProviderServices.GetBookings(r.RideId))
                {
                    Console.WriteLine($"\t{j++}.{booking.UserId}\t status:{booking.Status}");
                }
                if (rideProviderServices.GetBookings(r.RideId).Count==0)
                {
                    Console.WriteLine("Oops.! There are no Bookings for this ride");
                }
                Console.WriteLine("\n-----------------------------------------------------------------------------------------------------\n\n");
            }
        }

        private void AvailableRideOffers()
        {
            int i = 1;

            List<Ride> currentRides=rideProviderServices.GetAvailableRideOffers(UserServices.CurrentUser.UserId);

            if (currentRides != null)
            {
                Console.WriteLine("---------------------------------------------------------------------------------------------------\n\n");
                foreach (Ride r in currentRides)
                {
                    Console.WriteLine($"{i++}.Ridedate:{r.DateOfRide.Date}\n From:{r.Source}\tTo:{r.Destination} \nStartTime:{r.StartTime.TimeOfDay}\tEndTime:{r.EndTime.ToLocalTime()}\n" +
                        $"New Booking Requests:{rideProviderServices.GetNewBookingRequests(r.RideId).Count}");
                    Console.WriteLine("\n-----------------------------------------------------------------------------------------------------\n\n");
                }

                Console.WriteLine("Select a ride from above to approve booking requests");
                int.TryParse(Console.ReadLine(), out int rideNo);
                ApproveBookings(currentRides[rideNo - 1].RideId);
            }
            else
            {
                Console.WriteLine("No Ride Offers available");
            }
        }

        private void ApproveBookings(int rideId)
        {
            var bookings = rideProviderServices.GetNewBookingRequests(rideId);
            int i = 1;
            foreach(Booking booking in bookings)
            {
                User requester = userServices.GetUser(booking.UserId);
                Console.WriteLine($"{i++}.BookingId:{booking.BookingId}\nRequested BY:{requester.UserName}\t" +
                    $"Phone Number:{requester.PhoneNumber}\nPick Up Location:{booking.Source}\tDrop Location:{booking.Destination}\n");
            }
            if (bookings.Count == 0)
            {
                Console.WriteLine("Oops.! There are no Bookings for this ride");
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("Please Enter Booking Id you want to approve/reject:");

                    int.TryParse(Console.ReadLine(), out int bookingId);
                    do
                    {
                        Console.WriteLine("Enter 1 to approve\nEnter 2 to reject");

                        int.TryParse(Console.ReadLine(), out choice);
                        if (choice == 1)
                        {
                            rideProviderServices.ApproveBooking(bookingId,BookingStatus.Approved);
                            Console.WriteLine("Request Approved");
                        }
                        else if (choice == 2)
                        {
                            rideProviderServices.ApproveBooking(bookingId,BookingStatus.Rejected);
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

            int noOfSeats,index=1,providerId = UserServices.CurrentUser.UserId;

            string time;

            Console.Clear();
            if (rideProviderServices.IsCarLinked(providerId))
            {
                Console.WriteLine("List Of Cars Used:");

                var availableCars = rideProviderServices.GetCarsOfUser(providerId);

                Console.WriteLine("Please Choose One of the following cars:");
                for (int i = 0; i < availableCars.Count; i++)
                {
                    Console.WriteLine($"{i+1}.{availableCars[i].CarNo}");
                }
                Console.WriteLine("Enter your choice/enter 0 to add new car ");
                int.TryParse(Console.ReadLine(), out choice);
                if (choice==0)
                {
                    AddCar(providerId);
                }
                else
                {
                    carNo = availableCars[choice].CarNo;
                    capacity = availableCars[choice].Capacity;
                }
            }
            else
            {
                AddCar(providerId);
            }
           
            Console.WriteLine("Enter Starting Point");
            string source = Console.ReadLine();

            RideDataValidations rideDataValidations = new RideDataValidations();

            do
            {
                Console.WriteLine("Enter start time (HH:MM:SS) In 24 Hour Format");
                time = Console.ReadLine();
            } while (!rideDataValidations.IsValidTimeFormat(time));
            
            DateTime startTime = DateTime.ParseExact(time, "HH:mm:ss", CultureInfo.InvariantCulture);

            Console.WriteLine("Enter End Point");
            string destination = Console.ReadLine();

            DateTime endTime = startTime.AddSeconds(rideServices.GetDurationBetweenPlaces(source, destination));
            //do
            //{
            //    Console.WriteLine("Enter Reach time");
            //    time = Console.ReadLine();
            //} while (!rideDataValidations.IsValidTimeFormat(time));
            
            //DateTime endTime = DateTime.ParseExact(time, "HH:mm:ss", CultureInfo.InvariantCulture);

            do
            {
                Console.WriteLine("Enter No of seats available");
                noOfSeats = Convert.ToInt16(Console.ReadLine());
            } while (noOfSeats > capacity - 1 || noOfSeats < 1);
            


            Console.WriteLine("Enter intermediate places(seperated by ',')");
            List<string> viaPlaces = new List<string>(Console.ReadLine().Split(','));

            for(int i=0;i<viaPlaces.Count;i++)
            {
                viaPlaces[i] = viaPlaces[i].ToLower();
            }

            Console.WriteLine("Enter date of Journey(e.g. mm/dd/yyyy): ");
            dateOfRide = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Cost per Kilometer(Rupees.paise)");

            decimal amount = Convert.ToDecimal(Console.ReadLine());

            rideProviderServices.AddRide(providerId, carNo, source.ToLower(), destination.ToLower(), startTime, endTime, noOfSeats, viaPlaces, amount,dateOfRide);
            Console.WriteLine("Ride Added Sucessfully");
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
                capacity = Convert.ToInt16(Console.ReadLine());
            } while (capacity < 4 || capacity > 8);

            do
            {
                Console.WriteLine("select Car Type\n1.Ac\n 2.Non-AC");
                int.TryParse(Console.ReadLine(), out choice);
            } while (choice < 1 || choice > 2);
           
            carType = choice == 1 ? true : false;
            rideProviderServices.AddCar(carNo, carName, capacity, carType,providerId);
        }
    }

}
