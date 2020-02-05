using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CarPool.Models;
using CarPool.AplicationData;
using Newtonsoft.Json;

namespace CarPool.Services
{
    public class RideServices
    {
        readonly JourneyDetail journeyDetail;

        public List<string> Places;

        public RideServices()
        {
            string jsonString;
            try
            {
                using (StreamReader reader = new StreamReader(@"DistanceMatrix.json", System.Text.Encoding.UTF8))
                {
                    jsonString = reader.ReadToEnd();
                }
                journeyDetail = JsonConvert.DeserializeObject<JourneyDetail>(jsonString);
                Places = journeyDetail.DestinationPlaces;
            }
            catch (Exception e)
            {

                // ........what to do? how to handle exception.
            }
            
        }
        public bool IsValidRideId(int rideId)
        {
            return CarPoolData.Rides.FirstOrDefault(r => r.RideId == rideId) != null;
        }

        public int GetDistanceBetweenPlaces(string source,string destination,List<string> viaPoints)
        {
            // Returns the distance from source to destination via intermediate places(viaPoints)
            int distance = 0;
            string start = source;
            viaPoints.Add(destination);
            for (int i = 0; i < viaPoints.Count; i++)
            {
                var distanceMatrixData = journeyDetail.Rows[Places.IndexOf(viaPoints[i])].Elements[Places.IndexOf(start)];
                distance += distanceMatrixData.Distance.Value;
                start = viaPoints[i];
            }
            return distance / 1000;
        }

        public int GetDistanceBetweenPlaces(string source, string destination)
        {
            // returns the distance from source to destination.
            var distanceMatrixData = journeyDetail.Rows[Places.IndexOf(destination)].Elements[Places.IndexOf(source)];
            var distance = distanceMatrixData.Distance.Value;
            return distance / 1000;
        }

        public int GetDurationBetweenPlaces(string source,string destination)
        {
            var jsonData = journeyDetail.Rows[Places.IndexOf(destination)].Elements[Places.IndexOf(source)];
            return jsonData.Duration.Value;
        }

    }
}
