using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolDataBase
{
    class Repository
    {
        CarPoolDbContext dbContext = new CarPoolDbContext();
        public void AddUser()
        {
            
            dbContext.Users.Add(new User { UserId = 1, UserName = "sindhuja", PhoneNumber = "7093936071", Password = "1234", PetName = "hai", EmailAddress = "s.k@g.com" });
            dbContext.SaveChanges();
        }
        public List<User> GetAllUsers()
        {
            return dbContext.Users.ToList();
        }
    }
}
