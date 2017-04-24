using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Cynfo1._0.App_Start;
using Cynfo1._0.AzureUtils;
using Cynfo1._0.FirebaseModels;
using Cynfo1._0.Models;
using FireSharp.Response;
using Microsoft.AspNet.Identity;

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
            
            var userManager = _context.Users;
            string id = User.Identity.GetUserId();
            var activeUser = userManager.SingleOrDefault(u => u.Id == id);

            var beacons = from b in _context.Beacons
                where b.BussinessId.Equals(activeUser.CompanyIdentifier)
                select b;

            ViewBag.ActiveUser = activeUser.CompanyName;

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
        public async Task<ActionResult> Save(Beacon beacon, HttpPostedFileBase photo)
        {
            if (!ModelState.IsValid)
            {
                return View("BeaconForm", beacon);
            }

            var userManager = _context.Users;
            string activeUserId = User.Identity.GetUserId();
            var activeUser = userManager.SingleOrDefault(u => u.Id == activeUserId);
            if (beacon.Id == 0)
            {
                
                beacon.BussinessId = activeUser.CompanyIdentifier;
                beacon.BussinessName = activeUser.CompanyName;
                PhotoService photoservice = new PhotoService();
                beacon.AreaMediaUrl = await photoservice.UploadPhotoAsync(photo);
                _context.Beacons.Add(beacon);

          
            }
            else
            {
                var beaconInDb = _context.Beacons.SingleOrDefault(b => b.Id == beacon.Id);
                beaconInDb.AreaId = beacon.AreaId;
                beaconInDb.AreaName = beacon.AreaName;
                beaconInDb.MACAddress = beacon.MACAddress;
                beaconInDb.AreaMediaUrl = beacon.AreaMediaUrl;
                beaconInDb.BussinessId = activeUser.CompanyIdentifier;
                beaconInDb.BussinessName = activeUser.CompanyName;




            }
            _context.SaveChanges();

            FBarea fBarea = new FBarea()
            {
                name = beacon.AreaName,
                backgroundImage = beacon.AreaMediaUrl,
                id_Minor = beacon.AreaId,
                greetingMessage = beacon.GreetingMessage
                
            };

            if (fBarea.backgroundImage.IsEmpty())
            {
                fBarea.backgroundImage = "empty";


            }
            SetResponse response = await FirebaseInit.Firebaseclient.SetAsync("database/" + beacon.BussinessId +
                   "/areas/" + "ar" + beacon.BussinessId + beacon.Id, fBarea);


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