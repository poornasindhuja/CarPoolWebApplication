using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class Car
    {
        [Required]
        public string CarNo { get; set; }

        public string CarName { get; set; }

        public bool CarType { get; set; }

        [Required]
        [Range(4,8,ErrorMessage ="Car capacity should lies between 4 and 8")]
        public int Capacity { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public Car(string carNo, string carName, int capacity, bool carType,int ownerId)
        {
            this.CarNo = carNo;
            this.CarName = carName;
            this.Capacity = capacity;
            this.CarType = carType;
            this.OwnerId= ownerId;
        }
        public Car()
        {

        }
    }
}
