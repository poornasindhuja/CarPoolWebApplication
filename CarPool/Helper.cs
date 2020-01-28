using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool
{
    public class Helper
    {
        public string GetStringInput(string displayMessage, string errorMessage, Func<string, bool> Validationfunction)
        {
            do
            {
                Console.Write(displayMessage);
                var input = Console.ReadLine();
                if (!Validationfunction(input))
                {
                    Console.WriteLine(errorMessage);
                }
                else
                {
                    return input;
                }
            } while (true);
        }

        public int GetIntegerInput(string displayMessage, string errorMessage, Func<int, bool> Validationfunction)
        {
            do
            {
                Console.Write(displayMessage);
                int.TryParse(Console.ReadLine(), out int inputRootData);
                if (!Validationfunction(inputRootData))
                {
                    Console.WriteLine(errorMessage);
                }
                else
                {
                    return inputRootData;
                }
            } while (true);
        }

        public decimal GetDecimalInput()
        {
            decimal value;
            do
            { 
                if (Decimal.TryParse(Console.ReadLine(), out value))
                {
                    return value;
                }
                Console.WriteLine("Invalid Format");
            } while (true);           

        }

        public int GetLocation()
        {
            int i=1, choice;
            foreach (string place in Enum.GetNames(typeof(Places)))
            {
                Console.WriteLine($"{i++}.{place}");
            }
            do
            {
                int.TryParse(Console.ReadLine(), out choice);
                if(choice < 1 || choice > i-1)
                {
                    Console.WriteLine("Please choose valid option");
                }
                else
                {
                    break;
                }
            } while (true);           
            return choice;
        }
    }
}
