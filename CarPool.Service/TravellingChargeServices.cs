using System.Collections.Generic;
using System.IO;
using System.Linq;
using CarPool.Models;
using Newtonsoft.Json;

namespace CarPool.Services
{
    public class TravellingChargeServices
    {
        public List<string> CarTypes { get; private set; }

        public TravellCharges TravellCharges { get; private set; }

        public TravellingChargeServices()
        {
            string carsData;

            using (StreamReader reader = new StreamReader(@"RideCharges.json", System.Text.Encoding.UTF8))
            {
               carsData = reader.ReadToEnd();
            }

            TravellCharges = JsonConvert.DeserializeObject<TravellCharges>(carsData);
            CarTypes = TravellCharges.Cars.Select(p => p.CarType).ToList<string>();
        }
      
        public decimal GetMaximumCharge(int carType)
        {
            return TravellCharges.Cars.FirstOrDefault(c=>c.Id==carType).MaxCost;
        }
    }
}
