using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class JourneyDetails
    {
        public List<string> DestinationPlaces;
        public List<string> OrginPlaces;
        public List<distanceItem> rows;
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
        public List<data> elements;
        public distanceItem()
        {
            elements = new List<data>();
        }
    }
    public class data
    {
        public Duration duration;
        public Distance distance;
        public string status;
    }
    public class Duration
    {
        public string text;
        public int value;
    }
    public class Distance
    {
        public string text;
        public int value;
    }
}
