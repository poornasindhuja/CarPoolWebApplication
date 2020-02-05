using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarPool.Models;
using System.Threading.Tasks;

namespace CarPool.Database
{
    public class Repository
    {
        CarPoolDbContext carPoolDbContext = new CarPoolDbContext();
        public void AddUser()
        {
            carPoolDbContext.Users.Add(new User { UserId = 1, UserName = "sindhu", Password = "Hello", PetName = "hai" });
            carPoolDbContext.SaveChanges();
        }
        public List<User> GetUsers()
        {
            return carPoolDbContext.Users.ToList<User>();
        }
    }
}
