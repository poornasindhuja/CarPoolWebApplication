using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Validations
{
    class SignInValidations
    {

        // It checks whether the given phone number is valid type of phonenumber.
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length == 0)
            {
                Console.WriteLine("Phone Number should not be Empty");
                return false;
            }
            else if (phoneNumber.Length != 10)
            {
                Console.WriteLine("Phone Number Should Consist of 10 digits");
                return false;
            }
            return true;
        }

        internal static bool IsValidPassword(string password)
        {
            if (password.Length == 0)
            {
                Console.WriteLine("Password should not be empty");
                return false;
            }
            else if(password.Length<4)
            {
                Console.WriteLine("Password should have minimum 4 charactors");
                return false;
            }
            return true;
        }
    }
}
