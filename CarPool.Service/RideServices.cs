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
        public bool IsValidRideId(int rideId)
        {
            return CarPoolData.Rides.FirstOrDefault(r => r.RideId == rideId) != null ? true : false;
        }

        public int GetDistanceBetweenPlaces(string source,string destination,List<string> viaPoints)
        {
            // returns the distance from source to destination via intermediate places(viaPoints)
            int distance=0;
            string start = source;
            viaPoints.Add(destination);
            for(int i = 0; i <viaPoints.Count; i++)
            {
                var distanceMatrixData = GetRootInfoBetweenTwoPlaces(start, viaPoints.ElementAt(i));
                distance += distanceMatrixData.Distance.Value;
                start = viaPoints[i];
            }
            return distance/1000;
        }

        public int GetDistanceBetweenPlaces(string source, string destination)
        {
            // returns the distance from source to destination.
            var distanceMatrixData = GetRootInfoBetweenTwoPlaces(source, destination);
            var distance = distanceMatrixData.Distance.Value;
            return distance / 1000;
        }

        public int GetDurationBetweenPlaces(string source,string destination)
        {
            var jsonData = GetRootInfoBetweenTwoPlaces(source, destination);
            return jsonData.Duration.Value;
        }

        public DistanceMatrixElement GetRootInfoBetweenTwoPlaces(string source,string destination)
        {          
            string jsonString;
            try
            {
                using (StreamReader reader = new StreamReader(@"DistanceMatrix.json", System.Text.Encoding.UTF8))
                {
                    jsonString = reader.ReadToEnd();
                }
                JourneyDetail RootData = JsonConvert.DeserializeObject<JourneyDetail>(jsonString);
                return RootData.Rows[CarPoolData.Places.IndexOf(destination.ToLower())].Elements[CarPoolData.Places.IndexOf(source.ToLower())];
            }
            catch (Exception e)
            {
                // ........what to do? how to handle exception.
                return null;
            }          
        }
    }
}
