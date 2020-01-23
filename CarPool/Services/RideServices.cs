using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CarPool.AppData;
using CarPool.Models;
using Newtonsoft.Json;

namespace CarPool.Services
{
    public class RideServices
    {
       
        public bool IsvalidRideId(int rideId)
        {
            return CarPoolData.Rides.Find(r => r.RideId == rideId) != null ? true : false;
        }

        public int GetDistanceBetweenPlaces(string source,string destination)
        {
            var jsonData = GetRootInfoBetweenTwoPlaces(source, destination);
            int distance = jsonData.Distance.Value / 1000;
            return distance;
        }

        public int GetDurationBetweenPlaces(string source,string destination)
        {
            var jsonData = GetRootInfoBetweenTwoPlaces(source, destination);
            return jsonData.Duration.Value;
        }

        public Data GetRootInfoBetweenTwoPlaces(string source,string destination)
        {
           
            string jsonString;

            using (StreamReader reader = new StreamReader(@"D:\CarPooling\CarPool\CarPool\AppData\DistanceMatrix.json", System.Text.Encoding.UTF8))
            {
                jsonString = reader.ReadToEnd();
            }

            JourneyDetails data =JsonConvert.DeserializeObject<JourneyDetails>(jsonString);

            return data.Rows[CarPoolData.Places.IndexOf(destination.ToLower())].Elements[CarPoolData.Places.IndexOf(source.ToLower())];
        }
    }
}
