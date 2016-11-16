using Common.Extensions;
using Garage_2._1.DataAccess;
using Garage_2._1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Garage_2._1.Models.Exceptions;
using Garage_2._1.Models;

namespace Garage_2._0.Repositories
{
    public class ParkingspotRepository
    {
        private GarageContext dataBase = new GarageContext();

        public IEnumerable<Parkingspot> Parkingspots
        {
            get
            {
                foreach (Parkingspot parkingspot in dataBase.Parkingspots)
                {
                    yield return parkingspot;
                }
            }
        }

        /// <summary>
        /// It will add your vehicle to the parkingspot with ParkID or throw eather of 2 exceptions. 
        /// ParkingspotNotFoundException will be thrown if there is no corresponging parkingspot to the ID.
        /// VehicleAllreadyExistException will be thrown if there allready is an vehicle where you are trying to park.
        /// </summary>
        /// <param name="vehicle">The vehicle you want to park.</param>
        /// <param name="ParkID">The ID to the paringspot you want to park at.</param>
        public void Park(Vehicle vehicle, int ParkID)
        {
            Parkingspot tempSpot = (from spot in dataBase.Parkingspots
                                    where spot.Id == ParkID
                                    select spot).FirstOrDefault();

            if (tempSpot == null)
                throw new ParkingspotNotFoundException("Parkingspot (" + ParkID + ") does not exist in this garage.");

            if (tempSpot.Fordon != null)
                throw new VehicleAlreadyExistException("There is allready a vehicle in this spot. There cannot be two.");

            dataBase.Vehicles.AddOrUpdate(vehicle);
            tempSpot.Fordon = vehicle;
            dataBase.SaveChanges();
        }
        /// <summary>
        /// It will remove the vehicle at the parkingspot with ParkID or throw eather of 2 exceptions.
        /// ParkingspotNotFoundException will be thrown if there is no corresponging parkingspot to the ID.
        /// VehicleNotFoundException will be thrown if there are no vehicle in the spot where you want to take the vehicle from.
        /// </summary>
        /// <param name="ParkID">The ID to the parkingspot where the vehicle you want to leave with is at.</param>
        public void Leave(int ParkID)
        {
            Parkingspot tempSpot = (from spot in dataBase.Parkingspots
                                    where spot.Id == ParkID
                                    select spot).FirstOrDefault();
            if (tempSpot == null)
                throw new ParkingspotNotFoundException("Parkingspot (" + ParkID + ") does not exist in this garage.");

            if (tempSpot.Fordon == null)
                throw new VehicleNotFoundException("There is no vehicle in that parkingspot");

            tempSpot.Fordon = null;
            dataBase.SaveChanges();

        }

    }
}