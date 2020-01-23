﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.AppData;
using CarPool.Models;

namespace CarPool.Services
{
    public class RideProviderServices:RideServices,IRideProviderServices
    {

        public void AddRide(Ride ride)
        {
            ride.RideId = CarPoolData.Rides.Count + 1;
            ride.EndTime= ride.StartTime.AddSeconds(GetDurationBetweenPlaces(ride.Source,ride.Destination));
            CarPoolData.Rides.Add(ride);
        }

        public List<Ride> GetPastRideOffers(int userId)
        {
            return CarPoolData.Rides.FindAll(r => r.RideProviderId == userId && r.DateOfRide.Date < DateTime.Now.Date);
        }

        public List<Ride> GetAvailableRideOffers(int userId)
        {
            return CarPoolData.Rides.FindAll(r => r.RideProviderId == userId && r.DateOfRide.Date>= DateTime.Now.Date);
        }

        public void ApproveBooking(int bookingId,BookingStatus value)
        {
            
            Booking booking = CarPoolData.Bookings.Find(b => b.BookingId == bookingId);

            Ride currentRide =CarPoolData.Rides.Find(r => r.RideId == booking.RideId);

            if (booking != null && currentRide.NoOfSeatsAvailable > 0)
            {
                CarPoolData.Bookings.Find(b => b.BookingId == bookingId).Status = value;
                currentRide.NoOfSeatsAvailable = currentRide.NoOfSeatsAvailable - booking.NumberSeatsSelected;
            }
        }
       
        public void AddCar(Car car)
        {
            CarPoolData.Cars.Add(car);
        }

        public bool IsCarLinked(int providerId)
        {
            return CarPoolData.Cars.Count(c => c.OwnerId == providerId)!=0 ? true : false;
        }

        public List<Car> GetCarsOfUser(int userId)
        {
            return CarPoolData.Cars.FindAll(c => c.OwnerId == userId);
        }

        public List<Booking> GetBookings(int rideId)
        {
            return CarPoolData.Bookings.FindAll(b => b.RideId == rideId);
        }

        public List<Booking> GetNewBookingRequests(int rideId)
        {
            return CarPoolData.Bookings.FindAll(b => b.RideId == rideId && b.Status == BookingStatus.Pending);
        }
    }
}
