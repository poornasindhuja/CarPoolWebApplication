﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CarPool.Models;
using CarPool.AplicationData;
using Newtonsoft.Json;
using CarPool.Data;

namespace CarPool.Services
{
    public class RideServices
    {
        public List<string> Places;

        readonly Repository repository;

        public RideServices()
        {
            repository = new Repository();
        }
        public bool IsValidRideId(int rideId)
        {
            return repository.Get<Ride>(r => r.RideId == rideId) != null;
        }

        public decimal GetMaximumCharge(int carType)
        {
            return repository.Get<Data.Models.PriceLimit>(p => p.CarType == carType).Price;
        }
        public int GetDistanceBetweenPlaces(string source,string destination,List<string> viaPoints)
        {
            // Returns the distance from source to destination via intermediate places(viaPoints)
            int distance = 0;
            string start = source;
            viaPoints.Add(destination);
            for (int i = 0; i < viaPoints.Count; i++)
            {
                var sourceId = repository.Get<Data.Models.Place>(p => p.Name == source).Id;
                var destinationId= repository.Get<Data.Models.Place>(p => p.Name == destination).Id;
                var distanceMatrixData = repository.Get<Data.Models.RouteInformation>(r => r.Source == sourceId && r.Destination == destinationId);
                distance += distanceMatrixData.Distance;
                start = viaPoints[i];
            }
            return distance;
        }

        public int GetDistanceBetweenPlaces(string source, string destination)
        {
            var sourceId = repository.Get<Data.Models.Place>(p => p.Name == source).Id;
            var destinationId = repository.Get<Data.Models.Place>(p => p.Name == destination).Id;
            var distanceMatrixData = repository.Get<Data.Models.RouteInformation>(r => r.Source == sourceId && r.Destination == destinationId);
            var distance = distanceMatrixData.Distance;
            return distance;
        }

        public int GetDurationBetweenPlaces(string source,string destination)
        {
            var sourceId = repository.Get<Data.Models.Place>(p => p.Name == source).Id;
            var destinationId = repository.Get<Data.Models.Place>(p => p.Name == destination).Id;
            var distanceMatrixData = repository.Get<Data.Models.RouteInformation>(r => r.Source == sourceId && r.Destination == destinationId);
            return distanceMatrixData.Duration;
        }

    }
}
