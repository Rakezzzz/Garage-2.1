namespace Garage_2._1.Migrations
{
    using Garage_2._1.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Drawing;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Garage_2._1.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Garage_2._1.Models.ApplicationDbContext";
        }

        protected override void Seed(Garage_2._1.Models.ApplicationDbContext context)
        { 
            Vehicle v1 = new Vehicle() { RegNum = "AAA", SSN = "80", PaintColor = VehicleColor.Red, Type = VehicleType.Bus, NumberOfWheels = 1 };
            Vehicle v2 = new Vehicle() { RegNum = "BBB", SSN = "80", PaintColor = VehicleColor.Red, Type = VehicleType.Car, NumberOfWheels = 2 };
            Vehicle v3 = new Vehicle() { RegNum = "CCC", SSN = "8", PaintColor =  VehicleColor.Green, Type = VehicleType.Car, NumberOfWheels = 3 };
            Vehicle v4 = new Vehicle() { RegNum = "DDD", SSN = "8", PaintColor =  VehicleColor.Green, Type = VehicleType.Motorcycle, NumberOfWheels = 4 };
            Vehicle v5 = new Vehicle() { RegNum = "EEE", SSN = "3", PaintColor =  VehicleColor.Blue, Type = VehicleType.Trailer, NumberOfWheels = 5 };
            Vehicle v6 = new Vehicle() { RegNum = "FFF", SSN = "3", PaintColor =  VehicleColor.Green, Type = VehicleType.Car, NumberOfWheels = 6 };

            context.Vehicles.AddOrUpdate(
                v=>v.RegNum,
                v1,
                v2,
                v3,
                v4,
                v5,
                v6
                );


            Parkingspot ps1 = new Parkingspot() { ParkId = 1, RegNum = "AAA", SSN = "80", TimeOfRental = DateTime.Now, RentalTime = TimeSpan.FromMinutes(10)};
            Parkingspot ps2 = new Parkingspot() { ParkId = 2, RegNum = "BBB", SSN = "80", TimeOfRental = DateTime.Now, RentalTime = TimeSpan.FromMinutes(10) };
            Parkingspot ps3 = new Parkingspot() { ParkId = 3, RegNum = "CCC", SSN = "80", TimeOfRental = DateTime.Now, RentalTime = TimeSpan.FromMinutes(10) };
            Parkingspot ps4 = new Parkingspot() { ParkId = 4, RegNum = "DDD", SSN = "80", TimeOfRental = DateTime.Now, RentalTime = TimeSpan.FromMinutes(10) };
            Parkingspot ps5 = new Parkingspot() { ParkId = 5, RegNum = "EEE", SSN = "8", TimeOfRental = DateTime.Now, RentalTime = TimeSpan.FromMinutes(10) };
            Parkingspot ps6 = new Parkingspot() { ParkId = 6, RegNum = "FFF", SSN = "3", TimeOfRental = DateTime.Now, RentalTime = TimeSpan.FromMinutes(10) };




            context.Parkingspots.AddOrUpdate(
                park => park.ParkId,
                    ps1,
                    ps2,
                    ps3,
                    ps4,
                    ps5,
                    ps6
                );



            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
