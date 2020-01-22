using System;
using CarPool.Validations;
using CarPool.Services;
using CarPool.Models;

namespace CarPool
{
    class UserActions
    {
        int choice,userId;

        string phoneNumber, emailAddress, gender, password, userName, confirmPassword,petName;

        UserServices userServices;

        public UserActions()
        {
            userServices = new UserServices();
        }

        public void SignUp()
        {
            //User Name
            do
            {
                Console.WriteLine("Please Enter your Name");
                userName = Console.ReadLine();
                if (userName.Length== 0)
                {
                    Console.WriteLine("please Enter a valid name");
                }
            } while (userName.Length == 0);
            // PhoneNumber 
            do
            {
                do
                {
                    Console.WriteLine("Please Enter your Phone Number");
                    phoneNumber = Console.ReadLine();

                } while (!SignUpValidations.IsValidatePhoneNumber(phoneNumber));
            } while (userServices.IsExistingUser(phoneNumber));          
            // Email Address           
            do
            {
                Console.WriteLine("Please Enter your Email Address");
                emailAddress = Console.ReadLine();

            } while (!SignUpValidations.IsValidEmailAddress(emailAddress));

            // Address
            Console.WriteLine("Please Enter your Address");
            var address = Console.ReadLine();            

            // Gender
            while (true)
            {
                Console.WriteLine("Please Select your Gender by entering your choice\n1.Male\n2.Female\n3.Others");
                int.TryParse(Console.ReadLine(), out choice);
                if(choice>0 && choice<3)
                {
                    gender = choice == 1 ? "Male" : choice == 2 ? "Female" : "Other";
                    break;
                }
                Console.WriteLine("Please enter a valid option");
            }

            // Password
            do
            {
                Console.WriteLine("Please Set your password");
                password = Console.ReadLine();
            } while (!SignUpValidations.IsValidPassword(password));

            // Confirm password
            do
            {
                Console.WriteLine("Confirm your password");
                confirmPassword = Console.ReadLine();
            } while (password != confirmPassword);
            Console.WriteLine("Security Question");
            Console.WriteLine("Please Enter your first pet Name");
            petName = Console.ReadLine();
            while (true)
            {
                Console.WriteLine("Enter 1 to register");
                Console.WriteLine("Enter 2 to cancel");
                int.TryParse(Console.ReadLine(), out choice);
                if (choice == 1)
                {
                    userServices.SignUp(userName, phoneNumber, emailAddress, address, gender, password,petName);
                    Console.Clear();
                    Console.WriteLine("Your account has been sucessfully created.\n Press any key to continue");
                    Console.ReadKey();
                    break;
                }
                else if (choice == 2)
                {
                    break;
                }              
                Console.WriteLine("please enter a valid option");
            }
        }

        public void SignIn()
        {           
            int repeat = 0;

            Console.Clear();
            Console.WriteLine("----------------------------------Sign In-----------------------------------------\n");
            // Getting a valid phoneNumber from user to login
            do
            {
                do
                {
                    Console.WriteLine("Please enter your phone number /enter * to go back:");
                    phoneNumber = Console.ReadLine();
                    if (phoneNumber == "*")
                    {
                        return;
                    }
                } while (!SignInValidations.IsValidPhoneNumber(phoneNumber));
                if (!userServices.IsExistingUser(phoneNumber))
                {
                    Console.WriteLine("An user with this phone number was not registered.Try again");
                }
                else
                {
                    break;
                }
            } while (true);

            // Getting valid password from user 
            do
            {
                do
                {
                    Console.WriteLine("Please Enter your password");
                    password = Console.ReadLine();
                    if (password == "*")
                    {
                        return;
                    }
                } while (!SignInValidations.IsValidPassword(password));
                // It Checks whether user phoneNumber and password matched or not.
                if (userServices.SignIn(phoneNumber, password))
                {
                    userId = userServices.GetUser(phoneNumber).UserId;
                    UserOptions();
                    break;
                }
                else
                {
                    Console.WriteLine("wrong password");
                    repeat++;
                }
            } while (repeat < 3);
        }

        public void ForgotPassword()
        {
            Console.Clear();
            Console.WriteLine("Please enter your Phone Number");
            phoneNumber = Console.ReadLine();
            Console.WriteLine("Please enter your first petname");
            petName = Console.ReadLine();
            if (userServices.IsValidPetName(phoneNumber, petName))
            {
                Console.WriteLine("Set your new password");
                password = Console.ReadLine();
                Console.WriteLine("Confirm new password");
                confirmPassword = Console.ReadLine();
                if (password == confirmPassword)
                {
                    userServices.ResetPassword(phoneNumber, password);
                    Console.WriteLine("password sucessfully changed");
                }
            }
        }

        public void UserOptions()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please Choose one of the following options");
                Console.WriteLine("1.RideProvider");
                Console.WriteLine("2.RideTaker");
                Console.WriteLine("3.logout");
                int.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1:
                        RideProviderFunctionalites rideProviderFunctionalities = new RideProviderFunctionalites(userId);
                        rideProviderFunctionalities.ProviderOptions();
                        break;
                    case 2:
                        RideTakerFunctionalities rideTakerActions = new RideTakerFunctionalities(userId);
                        rideTakerActions.RideTakerOptions();
                        break;
                    case 3:
                        CarPoolMenu.DisplayMainMenu();
                        break;
                        
                }
            }   
        }
    }
}
