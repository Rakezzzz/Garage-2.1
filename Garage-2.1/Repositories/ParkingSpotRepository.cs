﻿using Common.Extensions;
using Garage_2._1.DataAccess;
using Garage_2._1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Garage_2._1.Models.Exceptions;

namespace Garage_2._1.Repositories
{
    public class ParkingspotRepository
    {
        private ApplicationDbContext dataBase = new ApplicationDbContext();

        public IEnumerable<Parkingspot> Parkingspots
        {
            get
            {
                List<Parkingspot> secondTempList = dataBase.Parkingspots.ToList();

                foreach (Parkingspot parkingspot in secondTempList)
                {
                    parkingspot.ParkedVehicle = GetVehicleByRegNum(parkingspot.RegNum);
                    parkingspot.Renter = GetPersonBySSN(parkingspot.SSN);
                    yield return parkingspot;
                }
            }
        }

        

        public Vehicle GetVehicleByRegNum(string regNum)
        {
            var temp = (from v in dataBase.Vehicles
                where v.RegNum == regNum
                select v).First();
            if (temp != null)
                return dataBase.Vehicles.Find(regNum);
            else
                throw new VehicleNotFoundException("There is no vehicle with that regnum in the garage.");
        }

        public Person GetPersonBySSN(string ssn)
        {
            return dataBase.Persons.Find(ssn);
        }
        /// <summary>
        /// It will add your vehicle to the parkingspot with ParkID or throw eather of 2 exceptions. 
        /// ParkingspotNotFoundException will be thrown if there is no corresponging parkingspot to the ID.
        /// SpotAllreadyOccupiedException will be thrown if there allready is an vehicle where you are trying to park.
        /// </summary>
        /// <param name="vehicle">The vehicle you want to park.</param>
        /// <param name="ParkID">The ID to the paringspot you want to park at.</param>
        public void Park(Vehicle vehicle, int ParkID)
        {
            Parkingspot tempSpot = (from spot in dataBase.Parkingspots
                                    where spot.ParkId == ParkID
                                    select spot).FirstOrDefault();

            if (tempSpot == null)
                throw new ParkingspotNotFoundException("Parkingspot (" + ParkID + ") does not exist in this garage.");

            if (tempSpot.ParkedVehicle != null)
                throw new SpotAllreadyOccupiedException("There is allready a vehicle in this spot. There cannot be two.");

            dataBase.Vehicles.AddOrUpdate(vehicle);
            tempSpot.ParkedVehicle = vehicle;
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
                                    where spot.ParkId == ParkID
                                    select spot).FirstOrDefault();
            if (tempSpot == null)
                throw new ParkingspotNotFoundException("Parkingspot (" + ParkID + ") does not exist in this garage.");

            if (tempSpot.ParkedVehicle == null)
                throw new VehicleNotFoundException("There is no vehicle in that parkingspot");

            tempSpot.ParkedVehicle = null;
            dataBase.SaveChanges();

        }

        /// <summary>
        /// Rents a specific parkingspot for a specific amount of time for a specific person or throws eather of 2 exceptions
        /// ParkingspotNotFoundException will be thrown if there is no corresponging parkingspot to the ID.
        /// SpotAllreadyOccupiedException will be thrown if someone is allready renting the parkingspot.
        /// </summary>
        /// <param name="ParkID">The ID to the paringspot you want to rent.</param>
        /// <param name="person">The person that are trying to rent the parkingspot.</param>
        /// <param name="timeSpan">The amount of time the person wants to rent the parkingspot.</param>
        public void Rent (int ParkID, Person person, TimeSpan timeSpan)
        {
            Parkingspot tempSpot = (from spot in dataBase.Parkingspots
                                    where spot.ParkId == ParkID
                                    select spot).FirstOrDefault();
            if (tempSpot == null)
                throw new ParkingspotNotFoundException("Parkingspot (" + ParkID + ") does not exist in this garage.");

            if (tempSpot.TimeOfRental != null)
                throw new SpotAllreadyOccupiedException("This parkingspot is not avalible for renting at this moment.");

            tempSpot.Renter = person;
            tempSpot.TimeOfRental = DateTime.Now;
            tempSpot.RentalTime = timeSpan;
            dataBase.SaveChanges();
        }
    }
}