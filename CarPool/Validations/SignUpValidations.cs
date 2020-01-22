using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;
using CarPool.Services;
using System.Text.RegularExpressions;

namespace CarPool.Validations
{
    static class SignUpValidations
    {
        
        public static bool IsValidPassword(string password)
        {
            if (password.Length == 0)
            {
                Console.WriteLine("Password should not be empty");
                return false;
            }
            else if (password.Length < 4 || password.Length > 16)
            {
                Console.WriteLine("password should consists of minimum 4 characters and maximum 16 characters");
                return false;
            }
            return true;
        }

        public static bool IsValidEmailAddress(string emailAddress)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4})(\]?)$";
            Regex re = new Regex(strRegex);
            if (!re.IsMatch(emailAddress))
            {
                Console.WriteLine("Invalid Email Address");
                return false;
            }               
            return true;
        }

        public static bool IsValidatePhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length == 0)
            {
                Console.WriteLine("Phone Number should not be empty");
                return false;
            }
            else
            {
                foreach (char ch in phoneNumber)
                {
                    if (ch < '0' || ch > '9')
                    {
                        Console.WriteLine("Phone Number should not contain alphabet or special characters");
                        return false;
                    }
                }

                if (phoneNumber.Length != 10)
                {
                    Console.WriteLine("Phone Number should contain exactly 10 digits");
                    return false;
                }
            }
            
            return true;
        }
    }
}
