using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using CarPool.Models;
using CarPool.AppData;
using CarPool.DataValidations;

namespace CarPool.Services
{

    public class UserServices:IUserServices
    {

        public  User CurrentUser;
       
        public bool IsCorrectPassword(string phoneNumber,string password)
        {
            return CarPoolData.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber && u.Password == password) != null;
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
            return CarPoolData.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber) != null ? true : false;
        }

        public bool SignUp(User user)
        {
            if(GenericValidator.Validate(user,out ICollection<ValidationResult> results))
            {
                user.UserId = CarPoolData.Users.Count + 1;
                CarPoolData.Users.Add(user);
                return true;
            }
            return false;           
        }

        public bool IsValidPetName(string phoneNumber,string petName)
        {
            return CarPoolData.Users.Find(u => u.PhoneNumber == phoneNumber && u.PetName == petName) != null;
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
