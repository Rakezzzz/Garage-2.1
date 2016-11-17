using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garage_2._1.Repositories;
using Garage_2._1.Models;
using Common.Extensions;

namespace Garage_2._1.Controllers
{




    //        Index - Show all parkingspots + sort

    //Your vehicles
    //Add vehicle
    //Rent spot
    //Edit vehicle
    //Att time
    //Checkout


    //.Where(v => v.ParkedVehicle.PropertyContains("Type", "Car")) Syntax för sökning?
    public class HomeController : Controller
    {

        private ParkingspotRepository _repo = new ParkingspotRepository();

        public ActionResult Index()
        {
            return View( 
                _repo.ParkingSpotsWithVehicles
                    .OrderBy(p => p.ParkedVehicle.Type)
                    .ThenBy(p => p.TimeOfRental));
        }

        //[Authorize]
        //public ActionResult Index()
        //{
        //    return View(_repo.Parkingspots
        //        .OrderByDescending(b => b.ParkId));
        //}


        /// <summary>
        /// Used when a Vehicle is parked in rented Parkingspot
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ParkVehicle(int id)
        {
            //list all vehicles known to belong to owner of Parkingslot
            //ViewBag.currentId = id;

            //Person owner = _repo.Parkingspots.SearchFromProperty("Id", id.ToString()).First();
            //var vehicles = _repo.Vehicles.Where(b => b.owner = owner);

            //return View(vehicles);
            List<Vehicle> test = new List<Vehicle>();

            return View(test);

            //Add option to create new vehicle belonging to owner

            //return RedirectToAction("AddVehicle", new {id = id});

        }

        public ActionResult AddVehicle(string id, int slotId)
        {

            //Parkingspot parkingSpot = _repo.Parkingspots.SearchFromProperty("Id", id.ToString()).First();
            //lägg till vehicle med id i parkingslot

            //Vehicle vehicle = _repo.Vehicles.SearchFromProperty("Id", id).First();
            //_repo.Park(vehicle, slotId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddVehicle(Vehicle vehicle)
        {
            _repo.Park(vehicle, ViewBag.currentId);
            return RedirectToAction("Index");
        }

        public ActionResult RentSpot(int id)
        {
            InfoViewModel model = new InfoViewModel(id);
            //Set owner & timespan for spot

            return View(model);
        }

        [HttpPost]
        public ActionResult RentSpot(InfoViewModel model)
        {

            if (ModelState.IsValid)
            {
                // Not used until we have a user
                // _repo.Rent(model.ParkingSpotId, model.Owner.SSN, model.Time);
                return RedirectToAction("Index");
            }

            //Set owner & timespan for spot

            return View(model);
        }

        public ActionResult EditVehicle(int id)
        {

            //get slot
            Parkingspot spot = _repo.Parkingspots.First(b => b.ParkId == id);

            return View(spot.ParkedVehicle);
        }

        [HttpPost]
        public ActionResult EditVehicle(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                //Update db with edited vehicle
            }
            return View();
        }

        public ActionResult AddTime()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult Leave(int id)
        {
            _repo.Leave(id);
            return RedirectToAction("Index");
        }

        public ActionResult Eviction(int id)
        {
            _repo.Eviction(id);
            return RedirectToAction("Index");
        }

        public ActionResult Search(string property = "Type", string value = "")
        {
            return 
                View(_repo.ParkingSpotsWithVehicles
                .Where(p => p.ParkedVehicle
                    .PropertyContains(property, value)));
        }
    }
}