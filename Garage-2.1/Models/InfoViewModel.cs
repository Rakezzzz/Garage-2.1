using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage_2._1.Models
{
    public class InfoViewModel
    {
        public int ParkingSpotId { get; private set; }
        public Person Owner { get; private set; }
        public IEnumerable Collection { get; private set; }

        public InfoViewModel(int parkingspotId, Person owner = null, IEnumerable collection = null)
        {
            this.ParkingSpotId = parkingspotId;
            this.Owner = (Person)owner.Clone();
            this.Collection = collection;
        }
    }

}