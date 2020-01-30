using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CarPool.Services;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CarPool.Validations
{
    public class SignInValidations:ISignInValidations
    {
        IUserServices userServices;

        private string phoneNumber;

        public SignInValidations()
        {
            userServices = new UserServices();
        }
        // It checks whether the given phone number is valid type of phonenumber.
        public bool IsValidPhoneNumber(string phoneNumber)
        {
            Regex regex = new Regex("^[6-9][0-9]{9}");
            if (regex.IsMatch(phoneNumber) && phoneNumber.Length == 10 && userServices.IsExistingUser(phoneNumber))
            {
                this.phoneNumber = phoneNumber;
                return true;           
            }
            return false;
        }

        public bool IsValidPassword(string password)
        {
            return userServices.IsCorrectPassword(phoneNumber, password);
        }
    }

}
