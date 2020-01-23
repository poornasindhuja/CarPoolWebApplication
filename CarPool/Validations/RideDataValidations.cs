using CarPool.AppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarPool.Validations
{
    public class RideDataValidations:IRideDataValidations
    {
        public bool IsValidTimeFormat(string time)
        {
            time += ":00";
            string strRegex = @"(?:[01]\d|2[0123]):(?:[012345]\d):(?:[012345]\d)";
            Regex re = new Regex(strRegex);
            if (!re.IsMatch(time))
            {
                Console.WriteLine("Invalid Time Format");
                return false;
            }

            return true;
        }
        public bool IsValidPlace(string place)
        {
            return CarPoolData.Places.Contains(place.ToLower());
        }

        public bool IsValidDateFormat(string dateInput)
        {
            string[] date = dateInput.Split('/');
            int.TryParse(date[0], out int dayOfMonth);
            int.TryParse(date[1], out int month);
            int.TryParse(date[2], out int year);
            if ((dayOfMonth < 1 || dayOfMonth > 31)||(month<0||month>12)||(year<1))
            {
                Console.WriteLine("Invalid date format");
                return false;
            }
            return true;
        }

        public bool IsValidNumberOfSeats(int count)
        {
            return false;
        }
    }
}
