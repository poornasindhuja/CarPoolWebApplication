using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services
{

    public class UserServices
    {
        public static List<User> users = new List<User>()
        {
            new User("1","sindhu","7093936071","sindhu@gmail.com","rpl","female","1234","pandu"),
            new User("2","jc","9876543210","jc@hotmail.com","hyd","female","9876","jc")
        };

        static int id = 3;

        private User currentUser;

        public User CurrentUser
        {
            get
            {
                return currentUser;
            }
        }


        public bool SignIn(string phoneNumber,string password)
        {
            currentUser = users.Find(u => u.PhoneNumber == phoneNumber);

            if (currentUser == null)
            {
                return false;
            }
            else if (password != currentUser.Password)
            {
                return false;
            }
            
            return true;
        }

        internal void AddRideProvider(string userName, string phoneNumber, string emailAddress, string address, string gender, string password)
        {
            throw new NotImplementedException();
        }

        internal void AddRideTaker(string userName, string phoneNumber, string emailAddress, string address, string gender, string password)
        {
            throw new NotImplementedException();
        }

        //public void xyz() { 

        //    int choice;

        //    Console.WriteLine("Please Choose one of the following options");
        //    Console.WriteLine("1.RideProvider");
        //    Console.WriteLine("2.RideTaker");
        //    Console.WriteLine("3.logout");
        //    int.TryParse(Console.ReadLine(), out choice);
        //    if (choice == 2)
        //    {
        //        bool repeat = true;
        //        while (repeat)
        //        {
        //            Console.WriteLine("Please Choose one of the following options");
        //            Console.WriteLine("1.Show rides");
        //            Console.WriteLine("2.Book ride");
        //            Console.WriteLine("3.Go Back");
        //            Console.WriteLine("4.logout");
        //            int.TryParse(Console.ReadLine(), out choice);
        //            switch (choice)
        //            {
        //                case 1:
        //                    rideServices.showRides();
        //                    break;
        //                case 2:
        //                    rideServices.showRides();
        //                    Console.WriteLine("Enter the ride Id want to book");
        //                    string rideNumber = Console.ReadLine();
        //                    RideTakerServices.BookRide(rideNumber, currentUser.UserId);
        //                    break;
        //                case 3:
        //                    repeat = false;
        //                    break;
        //                case 4: return;
        //                default:
        //                    Console.WriteLine("In correct option\n please choose a valid option");
        //                    break;
        //            }
        //        }

        //    }
        //    else if (choice == 1)
        //    {
        //        bool repeat = true;
        //        while (repeat)
        //        {
        //            Console.WriteLine("Please Choose one of the following options");
        //            Console.WriteLine("1.Add ride");
        //            Console.WriteLine("2.Current rides");
        //            Console.WriteLine("3.Go Back");
        //            Console.WriteLine("3.Logout");
        //            int.TryParse(Console.ReadLine(), out choice);

        //            switch (choice)
        //            {
        //                case 1:
        //                    services.AddRide(currentUser.UserId);
        //                    break;
        //                case 2:
        //                    services.showCurrentRides(currentUser.UserId);
        //                    break;
        //                case 3:
        //                    repeat = false;
        //                    break;
        //                case 4: return;
        //                default:
        //                    Console.WriteLine("Invalid option\n please choose a valid option");
        //                    break;
        //            }

        //        }

        //    }
        //    if (choice == 3)
        //    {
        //        return;
        //    }
        //}

        public bool IsExistingUser(string phoneNumber)
        {
            foreach (User user in UserServices.users)
            {
                if (user.PhoneNumber == phoneNumber)
                {
                    return true;
                }
            }
            return false;
        }

        internal void SignUp(string userName, string phoneNumber, string emailAddress, string address, string gender, string password,string petName)
        {
            
            users.Add(new User((id++).ToString(), userName, phoneNumber, emailAddress, address, gender, password,petName));
        }

        public bool IsValidPetName(string phoneNumber,string petName)
        {
            return users.Find(u => u.PhoneNumber == phoneNumber).PetName == petName;
        }

        internal void ResetPassword(string phoneNumber, string password)
        {
            users.Find(u => u.PhoneNumber == phoneNumber).Password = password;
        }
    }
}
