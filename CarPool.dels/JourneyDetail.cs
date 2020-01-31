﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class JourneyDetail
    {
        private List<string> destinationPlaces;
        private List<string> orginPlaces;
        private List<DistanceMatrixRow> rows;
        string status;

        public List<string> DestinationPlaces { get => destinationPlaces; set => destinationPlaces = value; }
        public List<string> OrginPlaces { get => orginPlaces; set => orginPlaces = value; }
        public List<DistanceMatrixRow> Rows { get => rows; set => rows = value; }
        public string Status { get => status; set => status = value; }

    }

    public class DistanceMatrixRow
    {
        private List<DistanceMatrixElement> elements;

        public List<DistanceMatrixElement> Elements { get => elements; set => elements = value; }
    }
    public class DistanceMatrixElement
    {
        private Duration duration;
        private Distance distance;
        private string status;


        public Distance Distance { get => distance; set => distance = value; }
        public Duration Duration { get => duration; set => duration = value; }
        public string Status { get => status; set => status = value; }
    }
    public class Duration
    {
        private string text;
        private int value;

        public string Text { get => text; set => text = value; }
        public int Value { get => value; set => this.value = value; }
    }
    public class Distance
    {
        private string text;
        private int value;

        public string Text { get => text; set => text = value; }
        public int Value { get => value; set => this.value = value; }
    }
}
