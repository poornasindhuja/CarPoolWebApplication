using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services
{
    public interface IUserServices
    {
        void SignIn(string phoneNumber);

        void SignUp(User user);

        User GetUser(int userId);

        bool IsExistingUser(string phoneNumber);

        bool IsValidPetName(string phoneNumber, string petName);

        void ResetPassword(string phoneNumber, string password);

        User GetUser(string phoneNumber);

        bool IsCorrectPassword(string phoneNumber, string password);
    }
}
