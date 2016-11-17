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
        [RegularExpression("^[A-Z]{3}[0-9]{3}$")]
        public string RegNum { get; set; }

        [Required]
        public string SSN { get; set; }

        [Required]
        public Color PaintColor { get; set; }

        [Required]
        public int NumberOfWheels { get; set; }
        [Required]
        public VehicleType Type { get; set; }

        public Vehicle()
        {

        }

        public Vehicle(string id, string ssn, VehicleColor paintColor, VehicleType type, int numberOfWheels)
        {
            this.RegNum = id;
            this.PaintColor = paintColor;
            this.SSN = ssn;
            this.Type = type;
            this.NumberOfWheels = numberOfWheels;
        }
    }

    public enum VehicleType
    {
        Car,
        Motorcycle,
        Bus,
        Trailer
    }
    
    public enum VehicleColor
    {
        Red,
        Green,
        Blue,
        Yellow
    }
}