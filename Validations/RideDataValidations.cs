using CarPool.AppRootData;
using System.Text.RegularExpressions;
using CarPool.Services;
using System;

namespace CarPool.Validations
{
    public class RideRootDataValidations:IRideRootDataValidations
    {
        public bool IsValidTimeFormat(string time)
        {
            time += ":00";
            string strRegex = @"(?:[01]\d|2[0123]):(?:[012345]\d):(?:[012345]\d)";
            Regex regex = new Regex(strRegex);
            return regex.IsMatch(time);
        }

        public bool IsValidPlace(string place)
        {
            return CarPoolRootData.Places.Contains(place.ToLower());
        }

        public bool IsValidDateFormat(string inputDate)
        {
            string dateFormat = @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/(20)\d\d$";
            Regex regex = new Regex(dateFormat);
            string[] date = inputDate.Split('/');
            return regex.IsMatch(inputDate);
        }
    }
}
