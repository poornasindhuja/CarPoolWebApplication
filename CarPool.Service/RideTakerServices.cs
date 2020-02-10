﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPool.Models;
using CarPool.Data;
using AutoMapper;
using CarPool.Service;

namespace CarPool.Services
{
    public class RideTakerServices:RideServices,IRideTakerServices
    {
        public GenericValidator genericValidator;

        public Repository repository;

        public RideTakerServices()
        {
            genericValidator = new GenericValidator();
            repository = new Repository();
        }
        public bool BookRide(Booking booking)
        {
            var ride = repository.Get<Data.Models.Ride>(r => r.RideId == booking.RideId).Map<Ride>();
            booking.StartTime = ride.StartTime.AddSeconds( GetDurationBetweenPlaces(ride.Source, booking.Source));
            booking.EndTime= ride.StartTime.AddSeconds(GetDurationBetweenPlaces(booking.Source, booking.Destination));
            var placesList = new List<string>
            {
                ride.Source.ToLower()
            };
            placesList.AddRange(ride.ViaPlaces);
            placesList.Add(ride.Destination);
            var root = placesList.GetRange(placesList.IndexOf(booking.Source) + 1, placesList.IndexOf(booking.Destination) - 1);
            booking.CostOfBooking = GetDistanceBetweenPlaces(booking.Source, booking.Destination,root) * ride.PricePerKilometer * booking.NumberSeatsSelected;
            booking.BookingDate = DateTime.Now;
            if (booking.NumberSeatsSelected <= ride.NoOfSeatsAvailable)
            {
                repository.Add<Data.Models.Booking>(MapperHelper.Map<Data.Models.Booking>(booking));
                return true;         
            }
            return false;
        }

        public List<Booking> GetAllBookings(int userId)
        {
            return MapperHelper.MapCollection<Data.Models.Booking,Booking>(repository.GetAll<Data.Models.Booking>(b => b.UserId == userId)).ToList();
        }

        public  List<Ride> GetAllRideOffers(int userId)
        {
            return MapperHelper.MapCollection<Data.Models.Ride,Ride>(repository.GetAll<Data.Models.Ride>(r => r.DateOfRide.Date >= DateTime.Now.Date && r.RideProviderId != userId && r.NoOfSeatsAvailable > 0)).ToList();
        }

        public List<Ride> SearchRides(string pickupLocation, string dropLocation,int userId)
        {
            var availableRides = new List<Ride>();

            // Returns all the ride offers available from pickup location to drop location.
            foreach (Ride ride in GetAllRideOffers(userId))
            {
                var route = ride.ViaPlaces;
                route.Insert(0, ride.Source);
                route.Insert(route.Count(), ride.Destination);
                if(route.Contains(pickupLocation)&& (route.IndexOf(pickupLocation) < route.IndexOf(dropLocation)))
                {
                    var maxSeatsAvailable = ride.NoOfSeatsAvailable;
                    bool isPickUpLocationExist=false;
                    foreach (var place in route)
                    {
                        if (place == dropLocation && maxSeatsAvailable > 0)
                        {
                            availableRides.Add(ride);
                        }
                        var seatsBeingFilled = repository.GetAll<Data.Models.Booking>(b => b.RideId == ride.RideId && b.Source == place && b.Status == (int)BookingStatus.Approved).Select(b => b.NumberSeatsSelected).Sum();
                        var seatsBeingEmpty = repository.GetAll<Data.Models.Booking>(b => b.RideId == ride.RideId && b.Destination == place && b.Status == (int)BookingStatus.Approved).Select(b => b.NumberSeatsSelected).Sum();
                        maxSeatsAvailable -= seatsBeingFilled;
                        maxSeatsAvailable += seatsBeingEmpty;                       
                        if (place==pickupLocation && maxSeatsAvailable > 0)
                        {
                            isPickUpLocationExist = true;
                        }
                        if (isPickUpLocationExist && maxSeatsAvailable <= ride.NoOfSeatsAvailable)
                        {
                            if (maxSeatsAvailable > 0)
                            {
                                ride.NoOfSeatsAvailable = maxSeatsAvailable;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return availableRides;
        }

        public Car GetCarDetails(string carNumber)
        {
            return repository.Get<Data.Models.Car>(c => c.CarNo == carNumber).Map<Car>();
        }

    }
}
