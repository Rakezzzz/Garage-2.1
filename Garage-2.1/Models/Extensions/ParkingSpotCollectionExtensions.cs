using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Garage_2._1.Models;

namespace Garage_2._1.Models.Extensions
{
    public static class ParkingSpotCollectionExtensions
    {
        public static IEnumerable<Parkingspot> Filter(this IEnumerable<Parkingspot> parkingSpots, VehicleType[] types, DateTime? from, DateTime? to)
        {
            return 
                parkingSpots
                    .Where(p => 
                        types.Contains(p.ParkedVehicle.Type) &&
                        from < p.TimeOfRental && p.TimeOfRental  < to);
        }
    }
}