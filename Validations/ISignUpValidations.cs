using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Validations
{
    public interface ISignUpValidations
    {
        bool IsValidPassword(string password);

        bool IsValidEmailAddress(string emailAddress);

        bool IsValidPhoneNumber(string phoneNumber);

        bool IsValidName(string name);
    }
}
