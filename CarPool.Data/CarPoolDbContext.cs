﻿using CarPool.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Data
{
    public class CarPoolDbContext:DbContext
    {      
        public DbSet<User> Users { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Ride> Rides { get; set; }

        public DbSet<PriceLimit> PriceLimits { get; set; }

        public DbSet<RouteInformation> RouteInformation { get; set; }

        public DbSet<Place> Places { get; set; }

        public void FixEfProviderServicesProblem()
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}