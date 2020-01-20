using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarPool.Validations
{
    class RideDataValidations
    {
        public bool IsValidTimeFormat(string time)
        {
            string strRegex = @"(?:[01]\d|2[0123]):(?:[012345]\d):(?:[012345]\d)";
            Regex re = new Regex(strRegex);
            if (!re.IsMatch(time))
            {
                Console.WriteLine("Invalid Time Format");
                return false;
            }

            return true;
        }
    }
}
