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
    class RideServices
    {
        public bool IsvalidRideId(int rideId)
        {
            return Database.Rides.Find(r => r.RideId == rideId) != null ? true : false;
        }

        public int GetDistanceBetweenPlaces(string source,string destination)
        {
            dynamic jsonData = GetRootInfoBetweenTwoPlaces(source, destination);
            int distance = jsonData.distance.value / 1000;
            return distance;
        }

        public int GetDurationBetweenPlaces(string source,string destination)
        {
            dynamic jsonData = GetRootInfoBetweenTwoPlaces(source, destination);
            return jsonData.duration.value / 60;
        }

        public dynamic GetRootInfoBetweenTwoPlaces(string source,string destination)
        {
            string jsonString, url = "https://maps.googleapis.com/maps/api/distancematrix/json?origins=";

            url += source + "&destinations=" + destination + "&mode=driving&key=AIzaSyB3mJ4gmoEDfSjFISRXmngMlZarF0s6XcY";

            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format(url));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            dynamic data =(JourneyDetails)JsonConvert.DeserializeObject(jsonString);

            return data.rows[0].elements[0];
        }
    }
}
