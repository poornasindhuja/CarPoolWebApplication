using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;
using CarPool.AppData;

namespace CarPool.Services
{

    public class UserServices
    {
        public RideProvider Provider;

        public static User CurrentUser;
       
        public bool SignIn(string phoneNumber,string password)
        {
            CurrentUser =Database.Users.Find(u => u.PhoneNumber == phoneNumber);

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
            return Database.Users.Find(u => u.UserId == userId);
        }

        public bool IsExistingUser(string phoneNumber)
        {
            foreach (User user in Database.Users)
            {
                if (user.PhoneNumber == phoneNumber)
                {
                    return true;
                }
            }
            return false;
        }

        internal void SignUp(string userName, string phoneNumber, string emailAddress, string address, string gender, string password,string petName)
        {
            
            Database.Users.Add(new User(Database.Users.Count+1, userName, phoneNumber, emailAddress, address, gender, password,petName));
        }

        public bool IsValidPetName(string phoneNumber,string petName)
        {
            return Database.Users.Find(u => u.PhoneNumber == phoneNumber).PetName == petName;
        }

        internal void ResetPassword(string phoneNumber, string password)
        {
            Database.Users.Find(u => u.PhoneNumber == phoneNumber).Password = password;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
