using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class User
    {
        public string UserId;

        public string UserName;

        public string PhoneNumber;

        public string EmailAddress;

        public string Address;

        public string Gender;

        public string PetName;

        public string CarNo;

        public string Password;

        public User(string userId, string userName, string phoneNumber, string emailAddress, string address, string gender, string password,string petName)
        {
            UserId = userId;
            UserName = userName;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            Address = address;
            Gender = gender;
            this.Password = password;
            this.PetName = petName;
        }

        public User()
        {
            
        }
    }
}
