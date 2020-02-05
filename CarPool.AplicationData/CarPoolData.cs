using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.AplicationData
{
    public static class CarPoolData
    {
        public static List<User> Users { get; set; }
        public static List<Booking> Bookings { get; set; }
        public static List<Car> Cars { get; set; }
        public static List<Ride> Rides { get; set; }
    }
}
