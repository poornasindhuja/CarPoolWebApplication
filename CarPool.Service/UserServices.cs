using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using CarPool.Models;
using CarPool.AplicationData;
using CarPool.Data;

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
            CurrentUser = repository.FindItem<User>(u => u.PhoneNumber == phoneNumber);
            if (CurrentUser != null && CurrentUser.Password == password)
            {
                IsCorrectPassword = true;
            }
            return IsCorrectPassword;
        }

        public User GetUser(int userId)
        {
            return repository.FindItem<User>(u => u.UserId == userId);
        }

        public bool IsExistingUser(string phoneNumber)
        {
            return repository.FindItem<User>(u => u.PhoneNumber == phoneNumber) != null;
        }

        public bool SignUp(User user)
        {
            if(GenericValidator.Validate(user,out ICollection<ValidationResult> results))
            {
                user.UserId = repository.GetAllData<User>().Count()+ 1;
                repository.Add<User>(user);
                //CarPoolData.Users.Add(user);
                return true;
            }
            return false;           
        }

        public bool IsValidPetName(string phoneNumber,string petName)
        {
            return repository.FindItem<User>(u => u.PhoneNumber == phoneNumber && u.PetName == petName) != null;
        }

        public void ResetPassword(string phoneNumber, string password)
        {
            //.......?
            CarPoolData.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber).Password = password;
        }

        public User GetUser(string phoneNumber)
        {
            return repository.FindItem<User>(u => u.PhoneNumber == phoneNumber);
        }

    }
}
