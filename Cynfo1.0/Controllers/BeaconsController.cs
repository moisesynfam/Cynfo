using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cynfo1._0.Models;

namespace Cynfo1._0.Controllers
{
    public class BeaconsController : Controller
    {
        private ApplicationDbContext _context;

        public BeaconsController()
        {
            _context = new ApplicationDbContext();
            
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

        }
        // GET: Beacons
        public ActionResult Index()
        {
            var beacons = _context.Beacons.ToList();

            return View(beacons);
        }


        public ActionResult Create()
        {
            Beacon beacon = new Beacon();

            return View("BeaconForm",beacon);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Beacon beacon)
        {
            if (!ModelState.IsValid)
            {
                return View("BeaconForm", beacon);
            }

            if (beacon.Id == 0)
            {
                _context.Beacons.Add(beacon);
            }
            else
            {
                var beaconInDb = _context.Beacons.SingleOrDefault(b => b.Id == beacon.Id);
                beaconInDb.AreaId = beacon.AreaId;
                beaconInDb.AreaName = beacon.AreaName;
                beaconInDb.MACAddress = beacon.MACAddress;
                beaconInDb.AreaMediaUrl = beacon.AreaMediaUrl;
                beaconInDb.BussinessId = beacon.BussinessId;
                beaconInDb.BussinessName = beacon.BussinessName;

            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Beacons");


        }


        public ActionResult Edit(int id)
        {


            var beacon = _context.Beacons.SingleOrDefault(b => b.Id == id);

            if (beacon == null)
                return HttpNotFound();

            return View("BeaconForm", beacon);

        }

    }
}