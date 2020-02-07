using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using CarPool.Models;
using CarPool.AplicationData;
using CarPool.Data;
using AutoMapper;
using CarPool.Service;

namespace CarPool.Services
{

    public class UserServices:IUserServices
    {

        public  User CurrentUser;
        public Repository repository;

        public UserServices()
        {
            repository = new Repository();
        }

        public bool SignIn(string phoneNumber,string password)
        {
            // If phonenumber and password matches then user will successfully login.
            bool IsCorrectPassword=false;
            CurrentUser =repository.FindItem<Data.Models.User>(u => u.PhoneNumber == phoneNumber).Map<User>();
            if (CurrentUser != null && CurrentUser.Password == password)
            {
                IsCorrectPassword = true;
            }
            return IsCorrectPassword;
        }

        public User GetUser(int userId)
        {
            return repository.FindItem<Data.Models.User>(u => u.UserId == userId).Map<User>();
        }

        public bool IsExistingUser(string phoneNumber)
        {
            var t = repository.FindItem<Data.Models.User>(u => u.PhoneNumber == phoneNumber);
            return t.Map<User>() != null;
        }

        public bool SignUp(User user)
        {
            if(GenericValidator.Validate(user,out ICollection<ValidationResult> results))
            {
                repository.Add<Data.Models.User>(MapperHelper.Map<Data.Models.User>(user));
                return true;
            }
            return false;           
        }

        public bool IsValidPetName(string phoneNumber,string petName)
        {
            return repository.FindItem<Data.Models.User>(u => u.PhoneNumber == phoneNumber && u.PetName == petName) != null;
        }

        public void ResetPassword(string phoneNumber, string password)
        {
            var user=repository.FindItem<Data.Models.User>(u => u.PhoneNumber == phoneNumber);
            user.Password = password;
            repository.Update<Data.Models.User>(user);
        }

        public User GetUser(string phoneNumber)
        {
            return repository.FindItem<Data.Models.User>(u => u.PhoneNumber == phoneNumber).Map<User>();
        }

    }
}
