﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Garage_2._1.Models
{
    public class Parkingspot
    {
        [Key]
        public int ParkId { get; set; }

        public string RegNum { get; set; }
        [ForeignKey("RegNum")]
        public Vehicle ParkedVehicle { get; set; }

        public string SSN { get; set; }
        [ForeignKey("SSN")]
        public Person Renter { get; set; }


        public DateTime? TimeOfRental { get;  set; }
        public TimeSpan? RentalTime { get;  set; }


        public Parkingspot()
        {
            //this.ParkedVehicle = null;
            //this.RentalTime = null;
            //this.TimeOfRental = null;
        }

        public Parkingspot(Vehicle iVehicle, DateTime? iTimeOfRental, TimeSpan? iRentalTime)
        {
            this.ParkedVehicle = iVehicle;
            this.TimeOfRental = iTimeOfRental;
            this.RentalTime = iRentalTime;
        }


        public void Rent(TimeSpan time)
        {
            this.RentalTime = time;
            this.TimeOfRental = DateTime.Now;
        }


        public object Clone()
        {
            return new Parkingspot((Vehicle)this.ParkedVehicle.Clone(), this.TimeOfRental, this.RentalTime);
        }
    }
}