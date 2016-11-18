using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garage_2._1.Repositories;
using Garage_2._1.Models;
using Microsoft.AspNet.Identity;
using Common.Extensions;
using Garage_2._1.Models.Extensions;

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
            //var viewModel = new FilterViewModel()
            //{
            //    Parkingspots = _repo.ParkingSpotsWithVehicles
            //        .OrderBy(p => p.ParkedVehicle.Type)
            //        .ThenBy(p => p.TimeOfRental)
            //};

            return View(_repo.ParkingSpotsWithVehicles
                             .OrderBy(p => p.ParkedVehicle.Type)
                             .ThenBy(p => p.TimeOfRental));
        }

        [Authorize]
        public ActionResult SuperIndex()
        {
            return View(_repo.Parkingspots.ToList());
        }

        public ActionResult ParkVehicle(int id, string regnum = null)
        {
            if (regnum != null)
            {
                var vehicle = _repo.GetVehicleByRegNum(regnum);
                _repo.Park(vehicle, id);
                return RedirectToAction("Index");
            }

            return View(
                new InfoViewModel(
                    id, 
                    _repo.GetAllCarsByUser(
                        User.Identity.GetUserId()
                )));
        }

        public ActionResult CreateParkingspot()
        {
            _repo.CreateParkingspot();
            return RedirectToAction("Index");
        }

        public ActionResult AddVehicle()
        {

            //Parkingspot parkingSpot = _repo.Parkingspots.SearchFromProperty("Id", id.ToString()).First();
            //lägg till vehicle med id i parkingslot

            //Vehicle vehicle = _repo.Vehicles.SearchFromProperty("Id", id).First();
            

            return View();
        }

        [HttpPost]
        public ActionResult AddVehicle(Vehicle vehicle)
        {

            if (ModelState.IsValid)
            {
                _repo.AddVehicle(vehicle, User.Identity.GetUserId());    
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult RentSpot(int id)
        {
            InfoViewModel model = new InfoViewModel(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult RentSpot(InfoViewModel model)
        {

            if (ModelState.IsValid)
            {
                _repo.Rent(model.ParkingSpotId, User.Identity.GetUserId(), model.Time);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult EditVehicle(int id)
        {
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