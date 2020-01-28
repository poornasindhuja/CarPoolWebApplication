using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services
{
    public interface IRideServices
    {
        bool IsvalidRideId(int rideId);

        int GetDistanceBetweenPlaces(string source, string destination);

        int GetDurationBetweenPlaces(string source, string destination);

    }
}
