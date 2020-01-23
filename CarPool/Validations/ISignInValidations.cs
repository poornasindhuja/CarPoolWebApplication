using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Validations
{
    interface ISignInValidations
    {
        bool IsValidPhoneNumber(string phoneNumber);

        bool IsValidPassword(string password);
    }
}
