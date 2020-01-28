using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CarPool.AppRootData;
using CarPool.Models;
using Newtonsoft.Json;

namespace CarPool.Services
{
    public class RideServices
    {
        public bool IsvalidRideId(int rideId)
        {
            return CarPoolRootData.Rides.FirstOrDefault(r => r.RideId == rideId) != null ? true : false;
        }

        public int GetDistanceBetweenPlaces(string source,string destination)
        {
            
            var jsonRootData = GetRootInfoBetweenTwoPlaces(source, destination);
            int distance = jsonRootData.Distance.Value / 1000;
            return distance;
        }

        public int GetDurationBetweenPlaces(string source,string destination)
        {
            var jsonRootData = GetRootInfoBetweenTwoPlaces(source, destination);
            return jsonRootData.Duration.Value;
        }

        public RootData GetRootInfoBetweenTwoPlaces(string source,string destination)
        {          
            string jsonString;

            using (StreamReader reader = new StreamReader(@"DistanceMatrix.json", System.Text.Encoding.UTF8))
            {
                jsonString = reader.ReadToEnd();
            }
            JourneyDetails RootData =JsonConvert.DeserializeObject<JourneyDetails>(jsonString);

            return RootData.Rows[CarPoolRootData.Places.IndexOf(destination.ToLower())].Elements[CarPoolRootData.Places.IndexOf(source.ToLower())];
        }
    }
}
