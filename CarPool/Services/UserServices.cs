using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;
using CarPool.AppData;

namespace CarPool.Services
{

    public class UserServices:IUserServices
    {

        public  User CurrentUser;
       
        public bool SignIn(string phoneNumber,string password)
        {
            CurrentUser =CarPoolData.Users.Find(u => u.PhoneNumber == phoneNumber);

            if (CurrentUser == null)
            {
                return false;
            }
            else if (password != CurrentUser.Password)
            {
                return false;
            }
            
            return true;
        }

        public User GetUser(int userId)
        {
            return CarPoolData.Users.Find(u => u.UserId == userId);
        }

        public bool IsExistingUser(string phoneNumber)
        {
            foreach (User user in CarPoolData.Users)
            {
                if (user.PhoneNumber == phoneNumber)
                {
                    return true;
                }
            }
            return false;
        }

        public void SignUp(string userName, string phoneNumber, string emailAddress, string address, string gender, string password,string petName)
        {
            
            CarPoolData.Users.Add(new User(CarPoolData.Users.Count+1, userName, phoneNumber, emailAddress, address, gender, password,petName));
        }

        public bool IsValidPetName(string phoneNumber,string petName)
        {
            return CarPoolData.Users.Find(u => u.PhoneNumber == phoneNumber).PetName == petName;
        }

        public void ResetPassword(string phoneNumber, string password)
        {
            CarPoolData.Users.Find(u => u.PhoneNumber == phoneNumber).Password = password;
        }

        public User GetUser(string phoneNumber)
        {
            return CarPoolData.Users.Find(u => u.PhoneNumber == phoneNumber);
        }
    }
}
