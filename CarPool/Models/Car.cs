using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class Car
    {
        public string CarNo { get; set; }
        public string CarName { get; set; }
        public bool CarType { get; set; }
        public int Capacity { get; set; }
        public int OwnerId { get; set; }

        public Car(string carNo, string carName, int capacity, bool carType,int ownerId)
        {
            this.CarNo = carNo;
            this.CarName = carName;
            this.Capacity = capacity;
            this.CarType = carType;
            this.OwnerId= ownerId;
        }
    }
}
