using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public string PetName { get; set; }

        public string CarNo { get; set; }

        public string Password { get; set; }

        public User(int userId, string userName, string phoneNumber, string emailAddress, string address, string gender, string password,string petName)
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
