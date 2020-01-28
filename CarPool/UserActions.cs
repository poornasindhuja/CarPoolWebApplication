using System;
using CarPool.Validations;
using CarPool.Services;
using CarPool.Models;

namespace CarPool
{
    public class UserActions:Helper
    {        
        public int choice,UserId;

        string phoneNumber,confirmPassword,password,petName;

        ISignInValidations signInValidations;

        IUserServices userServices;

        public User User { get; set; }
      
        public UserActions()
        {
            userServices = new UserServices();

            signInValidations = new SignInValidations();

            this.User = new User();
        }

        public void SignUp()
        {
            ISignUpValidations signUpValidations = new SignUpValidations();
            User.UserName = GetStringInput("Please Enter your Name: ", "Name should not be empty", signUpValidations.IsValidName);
            User.PhoneNumber = GetStringInput("Please Enter your phone number: ", "Invalid phone number", signUpValidations.IsValidPhoneNumber);
            User.EmailAddress = GetStringInput("Please Enter your email address: ","Invalid email address",signUpValidations.IsValidEmailAddress);       
            Console.Write("Please Enter your Address:");
            User.Address = Console.ReadLine();            

            Console.WriteLine("Please Select your Gender by entering your choice\n1.Male\n2.Female\n3.Others");
            while (true)
            {
                int.TryParse(Console.ReadLine(), out choice);
                if(choice>0 && choice<3)
                {
                    User.Gender = choice == 1 ? "Male" : choice == 2 ? "Female" : "Other";
                    break;
                }
                Console.WriteLine("Please enter a valid option");
            }
            User.Password = GetStringInput("Please set your password: ", "your password is week.try anthor password", signUpValidations.IsValidPassword);
            // Confirm password
            do
            {
                Console.Write("Confirm your password:");
                confirmPassword = Console.ReadLine();
            } while (User.Password != confirmPassword);
            Console.WriteLine("Security Question");
            Console.Write("Please Enter your first pet Name: ");
            User.PetName = Console.ReadLine();
            while (true)
            {
                Console.WriteLine("Enter 1 to register");
                Console.WriteLine("Enter 2 to cancel");
                int.TryParse(Console.ReadLine(), out choice);
                if (choice == 1)
                {
                    userServices.SignUp(User);
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
            Console.Clear();
            Console.WriteLine("----------------------------------Sign In-----------------------------------------\n");
            phoneNumber = GetStringInput("Please enter your phone number: ", "Invalid phonenumber", signInValidations.IsValidPhoneNumber);
            password = GetStringInput("Please Enter your password: ", "Wrong password", signInValidations.IsValidPassword);
            userServices.SignIn(phoneNumber);
            UserId = userServices.GetUser(phoneNumber).UserId;
            UserOptions();
        }

        public void ForgotPassword()
        {
            Console.Clear();
            phoneNumber = GetStringInput("Please enter your Phone Number: ", "Invalid phone number", signInValidations.IsValidPhoneNumber);
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
            else
            {
                Console.WriteLine("wrong answer\npress any key to go back");
                Console.ReadKey();
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
                        RideProviderFunctionalites rideProviderFunctionalities = new RideProviderFunctionalites(UserId);
                        rideProviderFunctionalities.ProviderOptions();
                        break;
                    case 2:
                        RideTakerFunctionalities rideTakerActions = new RideTakerFunctionalities(UserId);
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
