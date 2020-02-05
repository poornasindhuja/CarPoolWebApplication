﻿using CarPool.Models;
using CarPool.Data;
using System;

namespace CarPool
{
    public class CarPoolMenu
    {        
        static UserFunctionalities userFunctionalities;

        static int Choice;

        static void Main(string[] args)
        {
            userFunctionalities = new UserFunctionalities();
            DisplayMainMenu();
        }

        public static void DisplayMainMenu()
        {  
            while (true)
            {
                Console.Clear();
                Console.WriteLine("---------------WELCOME to CarPool----------------\n ");
                Console.WriteLine("1.SignIn\n2.SignUp \n3.Forgot Password \n4.close Application");
                Console.WriteLine("Please Enter your Choice");
                int.TryParse(Console.ReadLine(), out Choice);
                switch (Choice)
                {
                    case 1:
                        userFunctionalities.SignIn();
                        break;
                    case 2:
                        userFunctionalities.SignUp();
                        break;
                    case 3:
                        userFunctionalities.ForgotPassword();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Choice\n please choose a valid option");
                        break;
                }

            }
        }

    }

}
