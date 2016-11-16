using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Garage_2._1.Models
{
    public class Vehicle
    {
        [Key]
        public string RegNum { get; set; }

        public string SSN { get; set; }

        [ForeignKey("SSN")]
        public Person Owner { get; set; }


        [Required]
        public Color PaintColor { get; set; }
        [Required]
        public int NumberOfWheels { get; set; }
        [Required]
        public VehicleType Type { get; set; }

        public Vehicle()
        {

        }

        public Vehicle(string id, Person owner, Color paintColor, VehicleType type, int numberOfWheels)
        {
            this.RegNum = id;
            this.PaintColor = paintColor;
            this.Owner = owner;
            this.Type = type;
            this.NumberOfWheels = numberOfWheels;
        }

        public object Clone()
        {
            return new Vehicle(this.RegNum, (Person)this.Owner.Clone(), this.PaintColor, this.Type, this.NumberOfWheels);
        }
    }

    public enum VehicleType
    {
        Car,
        Motorcycle,
        Bus,
        Trailer
    }
    
}