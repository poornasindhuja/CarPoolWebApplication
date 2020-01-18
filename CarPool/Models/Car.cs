using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    class Car
    {
        public string CarNo;
        public string CarName;
        public string CarType;
        public int Capacity;

        public Car(string carNo, string carName, int capacity, string carType)
        {
            this.CarNo = carNo;
            this.CarName = carName;
            this.Capacity = capacity;
            this.CarType = carType;
        }
    }
}
