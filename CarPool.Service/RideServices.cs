using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CarPool.AppData;
using CarPool.Models;
using Newtonsoft.Json;

namespace CarPool.Services
{
    public class RideServices
    {
        JourneyDetail journeyDetail;
        public RideServices()
        {
            GetRootInfo();
        }
        public bool IsValidRideId(int rideId)
        {
            return CarPoolData.Rides.FirstOrDefault(r => r.RideId == rideId) != null;
        }

        public int GetDistanceBetweenPlaces(string source,string destination,List<string> viaPoints)
        {
            // returns the distance from source to destination via intermediate places(viaPoints)
            int distance=0;
            string start = source;
            viaPoints.Add(destination);
            for (int i = 0; i <viaPoints.Count; i++)
            {
                var distanceMatrixData = journeyDetail.Rows[CarPoolData.Places.IndexOf(viaPoints[i].ToLower())].Elements[CarPoolData.Places.IndexOf(start.ToLower())];
                distance += distanceMatrixData.Distance.Value;
                start = viaPoints[i];
            }
            return distance/1000;
        }

        public int GetDistanceBetweenPlaces(string source, string destination)
        {
            // returns the distance from source to destination.
            var distanceMatrixData = journeyDetail.Rows[CarPoolData.Places.IndexOf(destination.ToLower())].Elements[CarPoolData.Places.IndexOf(source.ToLower())];
            var distance = distanceMatrixData.Distance.Value;
            return distance / 1000;
        }

        public int GetDurationBetweenPlaces(string source,string destination)
        {
            var jsonData = journeyDetail.Rows[CarPoolData.Places.IndexOf(destination.ToLower())].Elements[CarPoolData.Places.IndexOf(source.ToLower())];
            return jsonData.Duration.Value;
        }

        public void GetRootInfo()
        {          
            string jsonString;
            try
            {
                using (StreamReader reader = new StreamReader(@"DistanceMatrix.json", System.Text.Encoding.UTF8))
                {
                    jsonString = reader.ReadToEnd();
                }
                journeyDetail = JsonConvert.DeserializeObject<JourneyDetail>(jsonString);
            }
            catch (Exception e)
            {
                // ........what to do? how to handle exception.
            }          
        }
    }
}
