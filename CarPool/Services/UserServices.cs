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
       
        public bool IsCorrectPassword(string phoneNumber,string password)
        {
            return CarPoolData.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber && u.Password == password) != null ? true : false;
        }

        public void SignIn(string phoneNumber)
        {
            CurrentUser =CarPoolData.Users.Find(u => u.PhoneNumber == phoneNumber);
        }

        public User GetUser(int userId)
        {
            return CarPoolData.Users.Find(u => u.UserId == userId);
        }

        public bool IsExistingUser(string phoneNumber)
        {
            return CarPoolData.Users.FirstOrDefault(u=>u.PhoneNumber==phoneNumber)!=null?true:false;
        }

        public void SignUp(User user)
        {
            user.UserId = CarPoolData.Users.Count + 1;
            CarPoolData.Users.Add(user);
        }

        public bool IsValidPetName(string phoneNumber,string petName)
        {
            return CarPoolData.Users.Find(u => u.PhoneNumber == phoneNumber && u.PetName == petName) != null ? true : false;
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
