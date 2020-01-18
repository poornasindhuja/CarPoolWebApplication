using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool
{
    public class User
    {
        public string UserId,UserName, PhoneNumber,EmailAddress,Address, Gender,PetName,CarNo,Password;

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
