using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;

namespace CarPool.AppData
{
    static class Database
    {
        static readonly DateTime date = new DateTime(2020, 2, 1);

        static readonly DateTime startTime = DateTime.ParseExact("12:00:00", "HH:mm:ss", CultureInfo.InvariantCulture);

        static DateTime endTime = DateTime.ParseExact("13:40:00", "HH:mm:ss", CultureInfo.InvariantCulture);

        public static List<Ride> Rides = new List<Ride>() {
            new Ride(1,1,"09 TS 071998","miyapur","madhapur",startTime,endTime,3,new List<string>(){"kothaguda","kondapur","hitech","durgamcheruvu"},4,date),
            new Ride(2,1,"10 TS 071930","miyapur","madhapur",startTime,endTime,3,new List<string>(){"kothaguda","kondapur","hitech","durgamcheruvu"},5,date),
            new Ride(3,2,"01 TS 045398","miyapur","madhapur",startTime,endTime,3,new List<string>(){"kothaguda","kondapur","hitech","durgamcheruvu"},4,date)
        };

        public static List<Car> Cars = new List<Car>()
        {
            new Car("09 TS 071998","Maruthi",4,false,1),
            new Car("10 TS 071930","Suzuki",4,true,1),
            new Car("01 TS 045398","Indica",4,true,2)
        };

        public static List<User> Users = new List<User>()
        {
            new User(1,"sindhu","7093936071","sindhu@gmail.com","rpl","female","1234","pandu"),
            new User(2,"jc","9876543210","jc@hotmail.com","hyd","female","9876","jc")
        };

        public static List<Booking> Bookings = new List<Booking>();
        
    }
}
