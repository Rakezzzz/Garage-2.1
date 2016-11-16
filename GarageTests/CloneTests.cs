using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Garage_2._1.Models;

namespace GarageTests
{
    [TestClass]
    public class CloneTests
    {
        [TestMethod]
        public void PersonCloneTest()
        {
            Person klas = new Person("Klas", "Anderstorp 3", "073 - 333 44 44");
            Person klasKopia = (Person)klas.Clone();
            klasKopia.Name = "Göran";

            Assert.AreEqual("Klas", klas.Name);
            Assert.AreEqual("Göran", klasKopia.Name);
        }

        [TestMethod]
        public void VehicleCloneTest()
        {
            Person klas = new Person("Klas", "Anderstorp 3", "073 - 333 44 44");
            Person göran = new Person("Göran", "Anderstorp 4", "073 - 334 33 44");

            Vehicle volkswagen = new Vehicle("VWV303", klas, System.Drawing.Color.Fuchsia, VehicleType.Car, 4);
            Vehicle seat = (Vehicle)volkswagen.Clone();

            seat.Owner = göran;

            Assert.AreEqual(klas.Name, volkswagen.Owner.Name);
            Assert.AreEqual(göran.Name, seat.Owner.Name);
        }

        [TestMethod]
        public void ParkingspotCloneTest()
        {
            Person klas = new Person("Klas", "Anderstorp 3", "073 - 333 44 44");
            Person göran = new Person("Göran", "Anderstorp 4", "073 - 334 33 44");

            Vehicle volkswagen = new Vehicle("VWV303", klas, System.Drawing.Color.Fuchsia, VehicleType.Car, 4);
            Vehicle seat = new Vehicle("SEA702", göran, System.Drawing.Color.Fuchsia, VehicleType.Car, 4);

            Parkingspot parkingspot1 = new Parkingspot(volkswagen, DateTime.Now, TimeSpan.FromMinutes(10));
            Parkingspot parkingspot2 = (Parkingspot)parkingspot1.Clone();

            parkingspot2.ParkedVehicle = seat;

            Assert.AreEqual("VWV303", parkingspot1.ParkedVehicle.RegNum);
            Assert.AreEqual("SEA702", parkingspot2.ParkedVehicle.RegNum);
        }
    }
}
