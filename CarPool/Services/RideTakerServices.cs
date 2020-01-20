using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;

namespace CarPool.Services
{
    class RideTakerServices
    {
        static int bookingId;

        public void BookRide(string rideId,string pickUpLocation,string dropLocation)
        { 
            Booking booking = new Booking((bookingId++).ToString(), rideId, pickUpLocation, dropLocation, UserServices.CurrentUser.UserId, DateTime.Now);

            //RideServices.AddBookingToRide(rideId, booking);
            RideServices.Bookings.Add(booking);
        }
        public List<Booking> GetAllBookings()
        {
            return RideServices.Bookings.FindAll(b => b.UserId == UserServices.CurrentUser.UserId);
        }

        public  List<Ride> getAllRideOffers()
        {
            return RideServices.Rides.FindAll(r => r.DateOfRide >= DateTime.Now && r.RideProviderId!=UserServices.CurrentUser.UserId);
        }

        internal List<Ride> searchRides(string pickupLocation, string dropLocation)
        {
            List<Ride> availableRides = new List<Ride>();
            // returns all the ride offers available from pickup location to drop location.
            foreach(Ride ride in getAllRideOffers())
            {
                if (ride.Source == pickupLocation)
                {
                    if (ride.ViaPlaces.Contains(dropLocation) || ride.Destination == dropLocation)
                    {
                        availableRides.Add(ride);
                    }
                }
                else if(ride.ViaPlaces.Contains(pickupLocation))
                {
                    if((ride.ViaPlaces.Contains(dropLocation)&& ride.ViaPlaces.IndexOf(dropLocation)>ride.ViaPlaces.IndexOf(pickupLocation))
                        || ride.Destination == dropLocation)
                    {
                        availableRides.Add(ride);
                    }
                }   
            }
            //return RideServices.Rides.FindAll(r => r.ViaPlaces.Contains(pickupLocation)||r.Source==pickupLocation && r.ViaPlaces.Contains(dropLocation)||r.Destination==dropLocation && r.ViaPlaces.IndexOf(pickupLocation)<r.ViaPlaces.IndexOf(dropLocation));   
            return availableRides;
        }
    }
}
