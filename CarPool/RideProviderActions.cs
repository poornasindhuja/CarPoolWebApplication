using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;
using CarPool.Services;

namespace CarPool
{
    class RideProviderActions
    {
        int choice;
        UserServices userServices = new UserServices();
        RideServices rideServices = new RideServices();
        public void providerOptions()
        {
            var taker = (RideTaker)userServices.CurrentUser;
            bool repeat = false;
            do
            {
                Console.WriteLine("Please Choose one of the following options");
                Console.WriteLine("1.Show rides");
                Console.WriteLine("2.Book ride");
                Console.WriteLine("3.Go Back");
                Console.WriteLine("4.logout");

                int.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1:
                        rideServices.showRides();
                        break;
                    case 2:
                        rideServices.showRides();
                        //Console.WriteLine("Enter the ride Id want to book");
                        //string rideNumber = Console.ReadLine();
                        //RideTakerServices.BookRide(rideNumber, currentUser.UserId);
                        break;
                    case 3:
                        repeat = false;
                        break;
                    case 4: return;
                    default:
                        Console.WriteLine("In correct option\n please choose a valid option");
                        break;
                }
            } while (repeat);
        }
    }
}
