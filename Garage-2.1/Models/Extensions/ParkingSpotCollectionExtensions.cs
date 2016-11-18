using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garage_2._1.Models;

namespace Garage_2._1.Models.Extensions
{
    public static class ParkingSpotCollectionExtensions
    {
        public static IEnumerable<Parkingspot> Filter(this IEnumerable<Parkingspot> parkingSpots, VehicleType? vehicleType = null, TimeSpan? timeSpan = null)
        {
            IEnumerable<Parkingspot> filteredCars = null;

            if (vehicleType == null && timeSpan.HasValue)
                filteredCars = from car in parkingSpots
                               where car.TimeOfRental < DateTime.Now.Subtract(timeSpan.Value)
                               select car;

            else if (timeSpan == null && vehicleType.HasValue)
            {
                filteredCars = from car in parkingSpots
                               where car.ParkedVehicle.Type == vehicleType
                               select car;
            }
            else if (vehicleType.HasValue && timeSpan.HasValue)
            {
                filteredCars = from car in parkingSpots
                               where (car.ParkedVehicle.Type == vehicleType) && (car.TimeOfRental < DateTime.Now.Subtract(timeSpan.Value))
                               select car;

            }
            return filteredCars;
        }
    }
}