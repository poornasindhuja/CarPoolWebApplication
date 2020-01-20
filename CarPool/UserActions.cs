using System;
using CarPool.Validations;
using CarPool.Services;
using CarPool.Models;

namespace CarPool
{
    class UserActions
    {
        int choice;

        string phoneNumber, emailAddress, gender, password, userName, confirmPassword,petName;

        UserServices userServices = new UserServices();

        public void SignUp()
        {
            SignUpValidations signUpValidations = new SignUpValidations();
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

                } while (!signUpValidations.IsValidatePhoneNumber(phoneNumber));
            } while (userServices.IsExistingUser(phoneNumber));          
            // Email Address           
            do
            {
                Console.WriteLine("Please Enter your Email Address");
                emailAddress = Console.ReadLine();

            } while (!signUpValidations.IsValidEmailAddress(emailAddress));

            // Address
            Console.WriteLine("Please Enter your Address");
            var address = Console.ReadLine();            

            // Gender
            while (true)
            {
                Console.WriteLine("Please Select your Gender\n1.Male\n2.Female\n3.Others");
                int.TryParse(Console.ReadLine(), out choice);
                if(choice>0 && choice<3)
                {
                    gender = choice == 1 ? "Male" : choice == 2 ? "Female" : "Other";
                    break;
                }
                Console.WriteLine("Please select valid option");
            }

            // Password
            do
            {
                Console.WriteLine("Please Set your password");
                password = Console.ReadLine();
            } while (!signUpValidations.IsValidPassword(password));

            // Confirm password
            do
            {
                Console.WriteLine("Confirm your password");
                confirmPassword = Console.ReadLine();
            } while (password != confirmPassword);
            Console.WriteLine("What is your first pet Name");
            petName = Console.ReadLine();
            while (true)
            {
                Console.WriteLine("Press 1 to register");
                Console.WriteLine("Press 2 to cancel");
                int.TryParse(Console.ReadLine(), out choice);
                if (choice == 1)
                {
                    userServices.SignUp(userName, phoneNumber, emailAddress, address, gender, password,petName);
                    Console.Clear();
                    Console.WriteLine("Your account has been sucessfully created.");
                    break;
                }
                else if (choice == 2)
                {
                    break;
                }              
                Console.WriteLine("please select a valid option");
            }
        }

        public void SignIn()
        {           
            string phoneNumber,password;

            while(true)
            {
                Console.Clear();
                Console.WriteLine("----------------------------------Sign In-----------------------------------------\n");
                // Getting a valid phoneNumber from user to login
                do
                {
                    Console.WriteLine("Please Enter your phone number:");
                    phoneNumber = Console.ReadLine();

                } while (!SignInValidations.IsValidPhoneNumber(phoneNumber));

                // Getting valid password from user 
                do
                {
                    Console.WriteLine("Please Enter your password:");
                    password = Console.ReadLine();

                } while (!SignInValidations.IsValidPassword(password));

                //It Checks whether user phoneNumber and password matched or not.
                if (userServices.SignIn(phoneNumber, password))
                {
                    userOptions();
                    break;
                }
                else
                {
                    Console.WriteLine("You have entered InCorrect phoneNumber or password\n1.Forgot Password \n2.Exit from login");
                    var choice=Console.ReadKey().KeyChar;
                    if (choice == '1')
                    {
                        forgotPassword();
                    }else if (choice == '2')
                    {
                        break;
                    }
                }
            }
        }

        public void forgotPassword()
        {
            Console.Clear();
            Console.WriteLine("Enter your Phone Number");
            phoneNumber = Console.ReadLine();
            Console.WriteLine("What is your first petname");
            petName = Console.ReadLine();
            if (userServices.IsValidPetName(phoneNumber, petName))
            {
                Console.WriteLine("set your new password");
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

        public void userOptions()
        {
            while (true)
            {
                Console.WriteLine("Please Choose one of the following options");
                Console.WriteLine("1.RideProvider");
                Console.WriteLine("2.RideTaker");
                Console.WriteLine("3.logout");
                int.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1:
                        RideProviderActions rideProviderActions = new RideProviderActions();
                        rideProviderActions.providerOptions();
                        break;
                    case 2:
                        RideTakerActions rideTakerActions = new RideTakerActions();
                        rideTakerActions.RideTakerOptions();
                        break;
                    case 3:
                        return;
                }
            }
            
        }
    }
}
