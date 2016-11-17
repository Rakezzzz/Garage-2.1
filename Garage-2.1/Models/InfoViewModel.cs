﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage_2._1.Models
{
    public class InfoViewModel
    {
        public int ParkingSpotId { get; set; }
        public List<Vehicle> Collection { get; set; }
        public TimeSpan Time {get; set;}

        public InfoViewModel()
        {
            
        }

        public InfoViewModel(int parkingspotId, List<Vehicle> collection = null, TimeSpan time = default(TimeSpan))
        {
            this.ParkingSpotId = parkingspotId;
            this.Collection = collection;
        }
    }

}