using System;
using System.Collections.Generic;
using CarPool.Services;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace CarPool.Validations
{    
    public class SignUpValidations:ISignUpValidations
    {
        IUserServices userServices;

        public SignUpValidations()
        {
            userServices = new UserServices();
        }

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            Regex regex = new Regex("^[1-9](0-9){9}");
            if(!userServices.IsExistingUser(phoneNumber))
            {
                return true;
            } 
            return false;
        }
    }
}
