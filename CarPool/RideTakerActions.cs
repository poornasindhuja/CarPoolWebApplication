using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Services;

namespace CarPool
{
    class RideTakerActions
    {
        public void RideTakerOptions()
        {
            bool repeat = true;
            int choice;

            RideProviderServices rideServices = new RideProviderServices();
            UserServices userServices = new UserServices();
            while (repeat)
            {
                Console.WriteLine("Please Choose one of the following options");
                Console.WriteLine("1.Add ride");
                Console.WriteLine("2.Current rides");
                Console.WriteLine("3.Go Back");
                Console.WriteLine("3.Logout");
                int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                         rideServices.AddRide(userServices.CurrentUser.UserId);
                        break;
                    case 2:
                        rideServices.showCurrentRides(userServices.CurrentUser.UserId);
                        break;
                    case 3:
                        repeat = false;
                        break;
                    case 4: return;
                    default:
                        Console.WriteLine("Invalid option\n please choose a valid option");
                        break;
                }

            }

        }
    }
}
