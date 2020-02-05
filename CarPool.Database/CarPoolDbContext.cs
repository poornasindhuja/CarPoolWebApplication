using System;
using System.Collections.Generic;
using System.Data.Entity;
using CarPool.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Database
{
    class CarPoolDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Car> Cars { get; set; }
    }
}
