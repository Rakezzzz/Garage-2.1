using Common.Extensions;
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
                    try
                    {
                        parkingspot.ParkedVehicle = GetVehicleByRegNum(parkingspot.RegNum);
                    }
                    catch (PersonNotFoundException)
                    {
                        parkingspot.ParkedVehicle = null;
                    }
                    catch (VehicleNotFoundException)
                    {
                        parkingspot.ParkedVehicle = null;
                    }

                    yield return parkingspot;
                }
            }
        }

        public IEnumerable<Parkingspot> ParkingSpotsWithVehicles
        {
            get
            {
                List<Parkingspot> secondTempList = dataBase.Parkingspots.ToList();

                foreach (Parkingspot parkingspot in secondTempList)
                {
                    try
                    {
                        parkingspot.ParkedVehicle = GetVehicleByRegNum(parkingspot.RegNum);
                    }
                    catch (PersonNotFoundException)
                    {
                        continue;
                    }
                    catch (VehicleNotFoundException)
                    {
                        continue;
                    }

                    yield return parkingspot;
                }
            }
        }

        public List<Vehicle> GetAllCarsByUser(string user)
        {
            if (dataBase.Users.Find(user) == null)
                throw new PersonNotFoundException("A person with that SSN doesnt exists!");

            var cars = (from myCars in dataBase.Vehicles
                        where myCars.SSN == user
                        select myCars).ToList();
            if (cars.Count <= 0)
                throw new VehicleNotFoundException("No vehicles was found!");

            return cars;



        }

        public Vehicle GetVehicleByRegNum(string regNum)
        {
            var temp = (from v in dataBase.Vehicles
                        where v.RegNum == regNum
                        select v).FirstOrDefault();
            if (temp != null)
                return dataBase.Vehicles.Find(regNum);
            else
                throw new VehicleNotFoundException("There is no vehicle with that regnum in the garage.");
        }

        public bool UserExists(string ssn)
        {
            if (dataBase.Users.Find(ssn) == null)
                return false;

            return true;
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

            if (tempSpot.RegNum == null)
                throw new VehicleNotFoundException("There is no vehicle in that parkingspot");

            tempSpot.RegNum = null;
            dataBase.SaveChanges();

        }

        /// <summary>
        /// Rents a specific parkingspot for a specific amount of time for a specific person or throws eather of 2 exceptions
        /// ParkingspotNotFoundException will be thrown if there is no corresponging parkingspot to the ID.
        /// SpotAllreadyOccupiedException will be thrown if someone is allready renting the parkingspot.
        /// </summary>
        /// <param name="ParkID">The ID to the parkingspot you want to rent.</param>
        /// <param name="user">The Account that are trying to rent the parkingspot.</param>
        /// <param name="timeSpan">The amount of time the person wants to rent the parkingspot.</param>
        public void Rent(int parkID, string user, TimeSpan timeSpan)
        {
            if (dataBase.Users.Find(user) == null)
                throw new PersonNotFoundException("A person with that SSN doesnt exists!");

            Parkingspot tempSpot = (from spot in dataBase.Parkingspots
                                    where spot.ParkId == parkID
                                    select spot).FirstOrDefault();
            if (tempSpot == null)
                throw new ParkingspotNotFoundException("Parkingspot (" + parkID + ") does not exist in this garage.");

            if (tempSpot.TimeOfRental != null)
                throw new SpotAllreadyOccupiedException("This parkingspot is not avalible for renting at this moment.");

            tempSpot.SSN = user;
            tempSpot.TimeOfRental = DateTime.Now;
            tempSpot.RentalTime = timeSpan;
            dataBase.SaveChanges();
        }



        /// <summary>
        /// Evicts someone from a specifik parkingspot or throws an exception.
        /// ParkingspotNotFoundException will be thrown if there is no corresponging parkingspot to the ID.
        /// </summary>
        /// <param name="parkID">The ID to the parkingspot you want to evict someone and thier vehicle from.</param>

        public void Eviction(int parkID)
        {
            Parkingspot tempSpot = (from spot in dataBase.Parkingspots
                                    where spot.ParkId == parkID
                                    select spot).FirstOrDefault();
            if (tempSpot == null)
                throw new ParkingspotNotFoundException("Parkingspot (" + parkID + ") does not exist in this garage.");
            tempSpot.RegNum = null;
            tempSpot.SSN = null;
            tempSpot.RentalTime = null;
            tempSpot.TimeOfRental = null;
            dataBase.SaveChanges();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="user"></param>
        void AddVehicle(Vehicle vehicle, string user)
        {
            if (dataBase.Vehicles.Find(vehicle.RegNum) != null)
                throw new VehicleAllreadyExistException();

            vehicle.SSN = user;
            dataBase.Vehicles.Add(vehicle);
            dataBase.SaveChanges();

        }


        public void CreateParkingspot()
        {
            int NumberOfParkingspots = (dataBase.Parkingspots.ToList().Count - 1);
            dataBase.Parkingspots.Add(new Parkingspot() { 
                ParkId = dataBase.Parkingspots.ToList()[NumberOfParkingspots].ParkId + 1,
                ParkedVehicle = null,
                RegNum = null,
                RentalTime = null,
                SSN = null,
                TimeOfRental = null
            });
            dataBase.SaveChanges();

        }
    }
}