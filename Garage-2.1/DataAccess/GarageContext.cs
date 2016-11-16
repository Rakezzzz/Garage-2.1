using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Garage_2._1.DataAccess
{
    public class GarageContext : DbContext
    {
        public DbSet<Models.Parkingspot> Parkingspots { get; set; }
        public DbSet<Models.Vehicle> Vehicles { get; set; }

        public GarageContext()
            : base("DefaultConnection")
        {
        }
    }
}