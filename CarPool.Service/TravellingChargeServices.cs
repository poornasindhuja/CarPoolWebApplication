using System.Collections.Generic;
using System.IO;
using System.Linq;
using CarPool.Models;
using Newtonsoft.Json;

namespace CarPool.Services
{
    public class TravellingChargeServices // remove if not needed
    {
        public TravellCharges TravellCharges { get; private set; }

        public TravellingChargeServices()
        {
            string carsData;

            using (StreamReader reader = new StreamReader(@"RideCharges.Json", System.Text.Encoding.UTF8))
            {
               carsData = reader.ReadToEnd();
            }

            TravellCharges = JsonConvert.DeserializeObject<TravellCharges>(carsData);
        }
      
        public decimal GetMaximumCharge(int carType)
        {
            return TravellCharges.Cars.FirstOrDefault(c=>c.Id==carType).MaxCost;
        }
    }
}
