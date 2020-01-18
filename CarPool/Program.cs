using System;

namespace CarPool
{
    class Program
    {
        static UserActions userActions = new UserActions();

        static int choice;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("---------------WELCOME to CarPool----------------\n 1.SignIn\n 2.SignUp \n 3.Forgot Password \n 4.close Application");
                Console.WriteLine("Please Enter your choice");
                int.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1: userActions.SignIn();
                        break;
                    case 2: userActions.SignUp();
                        break;
                    case 3: userActions.forgotPassword();
                        break;
                    case 4: Environment.Exit(0);
                        break;
                    default:Console.WriteLine("Invalid choice\n please choose a valid option:");
                        break;
                }
                
            }

        }

    }

}
