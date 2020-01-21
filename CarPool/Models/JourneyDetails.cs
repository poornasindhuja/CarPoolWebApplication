using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class JourneyDetails
    {
        List<string> DestinationPlaces;
        List<string> OrginPlaces;
        List<distanceItem> rows;
        string status;
        public JourneyDetails()
        {
            DestinationPlaces = new List<string>();
            OrginPlaces = new List<string>();
            rows = new List<distanceItem>();
        }
    }
    
    public class distanceItem
    {
        List<data> elements;
        public distanceItem()
        {
            elements=new List<data>
        }
    }
    public class data
    {
        Duration duration;
        Distance distance;
        string status;
    }
    public class Duration
    {
        string text;
        int value;
    }
    public class Distance
    {
        string text;
        int value;
    }
}
