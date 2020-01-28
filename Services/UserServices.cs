using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;
using CarPool.AppRootData;

namespace CarPool.Services
{

    public class UserServices:IUserServices
    {

        public  User CurrentUser;
       
        public bool IsCorrectPassword(string phoneNumber,string password)
        {
            return CarPoolRootData.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber && u.Password == password) != null ? true : false;
        }

        public void SignIn(string phoneNumber)
        {
            CurrentUser =CarPoolRootData.Users.Find(u => u.PhoneNumber == phoneNumber);
        }

        public User GetUser(int userId)
        {
            return CarPoolRootData.Users.Find(u => u.UserId == userId);
        }

        public bool IsExistingUser(string phoneNumber)
        {
            return CarPoolRootData.Users.FirstOrDefault(u=>u.PhoneNumber==phoneNumber)!=null?true:false;
        }

        public void SignUp(User user)
        {
            user.UserId = CarPoolRootData.Users.Count + 1;
            CarPoolRootData.Users.Add(user);
        }

        public bool IsValidPetName(string phoneNumber,string petName)
        {
            return CarPoolRootData.Users.Find(u => u.PhoneNumber == phoneNumber && u.PetName == petName) != null ? true : false;
        }

        public void ResetPassword(string phoneNumber, string password)
        {
            CarPoolRootData.Users.Find(u => u.PhoneNumber == phoneNumber).Password = password;
        }

        public User GetUser(string phoneNumber)
        {
            return CarPoolRootData.Users.Find(u => u.PhoneNumber == phoneNumber);
        }

    }
}
