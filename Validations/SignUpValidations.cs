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
    public class SignUpValidations:ISignUpValidations
    {
        IUserServices userServices;

        public SignUpValidations()
        {
            userServices = new UserServices();
        }

        public bool IsValidPassword(string password)
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

        public bool IsValidEmailAddress(string emailAddress)
        {
            string emailPattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4})(\]?)$";
            Regex re = new Regex(emailPattern);
            if (!re.IsMatch(emailAddress))
            {
                Console.WriteLine("Invalid Email Address");
                return false;
            }               
            return true;
        }

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            Regex regex = new Regex("^(0-9)*");
            if(regex.IsMatch(phoneNumber)&& phoneNumber.Length==10 && !userServices.IsExistingUser(phoneNumber))
            {
                return true;
            } 
            return false;
        }

        public bool IsValidName(string name)
        {
            return name.Length != 0 ? true : false;
        }
    }
}
