using CarPool.AppData;
using CarPool.Models;
using System;

namespace CarPool
{
    class CarPoolMenu
    {  
        
        static UserActions userActions;

        static int choice;

        static void Main(string[] args)
        {
            userActions = new UserActions();
            DisplayMainMenu();
        }

        public static void DisplayMainMenu()
        {  
            while (true)
            {
                Console.Clear();
                Console.WriteLine("---------------WELCOME to CarPool----------------\n ");
                Console.WriteLine("1.SignIn\n2.SignUp \n3.Forgot Password \n4.close Application");
                Console.WriteLine("Please Enter your choice");
                int.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1:
                        userActions.SignIn();
                        break;
                    case 2:
                        userActions.SignUp();
                        break;
                    case 3:
                        userActions.ForgotPassword();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice\n please choose a valid option");
                        break;
                }

            }
        }

    }

}
