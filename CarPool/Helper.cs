using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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

        public string GetStringMatch(string displayMessage,string errorMessage, string pattern)
        {
            do
            {
                Console.Write(displayMessage);
                var input = Console.ReadLine();
                Regex regex = new Regex(pattern);
                if (regex.IsMatch(input))
                {
                    return input;
                }
                Console.WriteLine(errorMessage);
            } while (true);
        }

        public string GetHashCode(string data)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] hashbytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashbytes.Length; i++)
                {
                    builder.Append(hashbytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public int GetIntegerInRange(string displayMessage, string errorMessage, int minimumValue, int maximumValue)
        {
            // It will return a value between minimum value and maximum value.
            do
            {
                Console.Write(displayMessage);
                if(int.TryParse(Console.ReadLine(), out int inputData))
                {
                    if (inputData < minimumValue || inputData > maximumValue)
                    {
                        Console.WriteLine(errorMessage);
                    }
                    else
                    {
                        return inputData;
                    }
                }               
            } while (true);
        }

        public int GetUserChoiceInList(List<string> values)
        {
            // It will display all the strings in list and returns the the position of string which user has choosed
            int i = 1;
            foreach (string value in values)
            {
                Console.WriteLine($"{i++}.{value}");
            }
            do
            {
                int.TryParse(Console.ReadLine(), out int Choice);
                if (Choice >= 1 || Choice < i)
                {
                    return Choice - 1;
                }
            } while (true);
        }

        public int GetUserChoiceInEnum<T>()
        {
            // It will display all the strings in enum (of type T) and the the position of string which user has choosed
            int i = 1;
            foreach (string value in Enum.GetNames(typeof(T)))
            {
                Console.WriteLine($"{i++}.{value}");
            }
            do
            {
                int.TryParse(Console.ReadLine(), out int Choice);
                if (Choice >= 1 && Choice < i)
                {
                    return Choice - 1;
                }
                Console.WriteLine("Invalid Data");
            } while (true);
        }
    }
}
