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
        int choice;

        string carNo;

        string carName;

        int capacity;

        bool carType;

        UserServices userServices = new UserServices();

        RideServices rideServices = new RideServices();

        RideProviderServices rideProviderServices = new RideProviderServices();

        public void providerOptions()
        {
            //userServices.UserToProvider();
            bool repeat = true;
            do
            {
                Console.WriteLine("Please Choose one of the following options\n" +
                    "1.Add Ride\n" +
                    "2.Current Rides\n" +
                    "3.pastRides" +
                    "4.Go Back\n" +
                    "5.logout");

                int.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1:AddRide();
                        break;
                    case 2:currentRides();
                        break;
                    case 3:showPastRides();
                        break;
                    case 4:repeat = false;
                        break;
                    case 5: return;
                    default:Console.WriteLine("In correct option\n please choose a valid option");
                        break;
                }
            } while (repeat);
        }

        private void showPastRides()
        {
            int i = 1;
            Console.WriteLine("---------------------------------------------------------------------------------------------------\n\n");
            List<Ride> currentRides = rideProviderServices.PastRides(UserServices.CurrentUser.UserId);
            foreach (Ride r in currentRides)
            {
                Console.WriteLine($"{i++}.RideId:{r.RideId}\n From:{r.Source}\tTo:{r.Destination} \nStartTime:{r.StartTime.ToShortTimeString()}\tEndTime:{r.EndTime.ToShortTimeString()}");
                int j = 1;
                foreach (Booking booking in rideServices.GetBookings(r.RideId))
                {
                    Console.WriteLine($"\t{j++}.{booking.UserId}\t status:{booking.Status}");
                }
                if (r.Bookings.Count == 0)
                {
                    Console.WriteLine("Oops.! There are no Bookings for this ride");
                }
                Console.WriteLine("\n-----------------------------------------------------------------------------------------------------\n\n");
            }
        }

        private void currentRides()
        {
            int i = 1, rideNo;

            List<Ride> currentRides=rideProviderServices.CurrentRides(UserServices.CurrentUser.UserId);

            Console.WriteLine("---------------------------------------------------------------------------------------------------\n\n");
            foreach (Ride r in currentRides)
            {
                Console.WriteLine($"{i++}.Ridedate:{r.DateOfRide.Date}\n From:{r.Source}\tTo:{r.Destination} \nStartTime:{r.StartTime.TimeOfDay}\tEndTime:{r.EndTime.ToLocalTime()}\n" +
                    $"Booking Requests:{rideServices.GetNewBookingRequests(r.RideId).Count}");
                Console.WriteLine("\n-----------------------------------------------------------------------------------------------------\n\n");
            }

            Console.WriteLine("Select a ride from above to approve booking requests");
            int.TryParse( Console.ReadLine(),out rideNo);
            //ApproveBooking(rideNo-1, currentRides);
            ApproveBookings(currentRides[rideNo - 1].RideId);
            //ApproveBooking(rideServices.GetBookings(currentRides[rideNo-1].RideId));
        }

        private void ApproveBookings(string rideId)
        {
            var bookings = rideServices.GetNewBookingRequests(rideId);
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
            while (true)
            {
                Console.WriteLine("Please Enter Booking Id you want to approve/reject:");

                var bookingId = Console.ReadLine();

                do
                {
                    Console.WriteLine("Enter 1 to approve\nEnter 2 to reject");

                    int approve = Convert.ToInt16(Console.ReadLine());
                    if (approve == 1)
                    {
                        rideProviderServices.ApproveBooking(bookingId, true);
                        Console.WriteLine("Request Approved");
                    }
                    else if (approve == 2)
                    {
                        rideProviderServices.ApproveBooking(bookingId, false);
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
                int.TryParse(Console.ReadLine(),out choice);
                if (choice == 2)
                {
                    break;
                }
            }
           
        }

        //private void ApproveBooking(int rideNo, List<Ride> currentRides)
        //{
        //    var bookings = currentRides[rideNo].Bookings;
        //    int i = 1;
        //    foreach (Booking booking in bookings)
        //    {
        //        User requester = userServices.GetUser(booking.UserId);
        //        Console.WriteLine($"{i++}.Requested BY:{requester.UserName}\nPhone Number:{requester.PhoneNumber}\t \nPick Up Location:{booking.Source}\tDrop Location:{booking.Destination}\n" +
        //            $"status:{booking.Status}");
        //    }
        //    if (bookings.Count == 0)
        //    {
        //        Console.WriteLine("Oops.! There are no Bookings for this ride");
        //    }

        //    while (true)
        //    {
        //        Console.WriteLine("Please Enter Booking Id you want to approve/reject:");

        //        var bookingId = Console.ReadLine();

        //        Console.WriteLine("Press 1 to approve\n press 2 to reject");

        //        int approve = Convert.ToInt16(Console.ReadLine());

        //        if (approve == 1)
        //        {
        //            rideProviderServices.ApproveBooking(bookingId, true);
        //            Console.WriteLine("Request Approved");
        //        }
        //        else if (approve == 2)
        //        {
        //            rideProviderServices.ApproveBooking(bookingId, false);
        //            Console.WriteLine("Request Rejected");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Invalid Option");
        //        }
        //        Console.WriteLine("Enter 1 to continue approving");
        //        Console.WriteLine("Enter 2 to Go Back");
        //        int.TryParse(Console.ReadLine(), out choice);
        //        if (choice == 2)
        //        {
        //            break;
        //        }
        //    }

        //}

        public void AddRide()
        {
            DateTime dateOfRide;

            string providerId = UserServices.CurrentUser.UserId;

            string time;

            int noOfSeats;

            if (rideProviderServices.IscarLinked(providerId))
            {
                Console.WriteLine("Enter 1 to continue with this car \n" +
                    "Enter 2 to Add new car");
                int.TryParse(Console.ReadLine(), out choice);
                if (choice == 2)
                {
                    AddCar(providerId);
                }
            }
            AddCar(providerId);

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

            do
            {
                Console.WriteLine("Enter Reach time");
                time = Console.ReadLine();
            } while (!rideDataValidations.IsValidTimeFormat(time));
            
            DateTime endTime = DateTime.ParseExact(time, "HH:mm:ss", CultureInfo.InvariantCulture);

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

            Console.WriteLine("Enter date of Journey(e.g. 10 / 22 / 1987): ");
            try
            {
                dateOfRide = DateTime.Parse(Console.ReadLine());
            }
            catch(Exception e)
            {
                Console.WriteLine("Invalid Time Format");
            }
             
     
            Console.WriteLine("Cost per Kilometer(Rupees.paise");
            Decimal amount = Convert.ToDecimal(Console.ReadLine());

            rideProviderServices.AddRide(providerId, carNo, source.ToLower(), destination.ToLower(), startTime, endTime, noOfSeats, viaPlaces, amount,dateOfRide);

            Console.WriteLine("Ride Added Sucessfully");
        }

        public void AddCar(string providerId)
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
            RideProviderServices rideProviderServices = new RideProviderServices();
            rideProviderServices.AddCar(carNo, carName, capacity, carType,providerId);
        }
    }
}
