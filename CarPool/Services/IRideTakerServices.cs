using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;

namespace CarPool.Services
{
    public interface IRideTakerServices:IRideServices
    {
        void BookRide(Booking booking);

        List<Booking> GetAllBookings(int userId);

        List<Ride> GetAllRideOffers(int userId);

        IList<Ride> SearchRides(string pickupLocation, string dropLocation, int userId);

        Car GetCarDetails(string carNumber);
    }
}
