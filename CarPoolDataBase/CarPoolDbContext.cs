using System;
using System.Collections.Generic;
using CarPool.Models;
using System.Data.Entity;

namespace CarPoolDataBase
{
    public class CarPoolDbContext :DbContext
    {
        public List<User> Users { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<Car> Cars { get; set; }
        public List<Ride> Rides { get; set; }
    }
}
