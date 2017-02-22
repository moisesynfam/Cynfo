using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Cynfo1._0.AzureUtils;
using Cynfo1._0.Models;
using Cynfo1._0.ViewModels;
using FirebaseSharp.Portable;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNet.Identity;

namespace Cynfo1._0.Controllers
{
    public class AdvertisementsController : Controller
    {

        private ApplicationDbContext _context;

        private IFirebaseConfig FBconfig;

        private IFirebaseClient FBclient;

        public AdvertisementsController()
        {
            
            _context = new ApplicationDbContext();
            FBconfig = new FirebaseConfig
            {
                AuthSecret = "mKIYPbYtV1kjS975Ck31R5k3gMN51EY7ronuz1x8",
                BasePath = "https://cynfotest.firebaseio.com/"
            };

            FBclient = new FirebaseClient(FBconfig);


        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: Advertisements
        public async Task<ActionResult> Index()
        {
            var userManager = _context.Users;
            string activeUserId = User.Identity.GetUserId();
            var activeUser = userManager.SingleOrDefault(u => u.Id == activeUserId);

            var advertisements = from a in _context.Advertisements
                where a.CompanyID.Equals(activeUser.CompanyIdentifier)
                select a;


            var beacons = from b in _context.Beacons
                          where b.BussinessId.Equals(activeUser.CompanyIdentifier)
                          select b;





            var adsViewModel = new AdsViewModel()
            {
                Advertisements = advertisements,
                Beacons = beacons
           
            };
            

            return View(adsViewModel);
        }

        public ActionResult Create()
        {
            var userManager = _context.Users;
            string activeUserId = User.Identity.GetUserId();
            var activeUser = userManager.SingleOrDefault(u => u.Id == activeUserId);


            var beacons = from b in _context.Beacons
                          where b.BussinessId.Equals(activeUser.CompanyIdentifier)
                          select b;
            var viewModel = new AdFormViewModel()
            {
                Advertisement = new Advertisement(),
                Beacons = beacons
            };

            return PartialView("_AdvertisementsFormPartial",viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(Advertisement advertisement, HttpPostedFileBase photo)
        {
            if (!ModelState.IsValid)
            {
                var beacons = _context.Beacons.ToList();
                var viewModel = new AdFormViewModel()
                {

                    Advertisement = advertisement,
                    Beacons = beacons
                };


                return View("AdvertisementsForm", viewModel);

            }

            if (advertisement.Id == 0)
            {
                PhotoService photoservice = new PhotoService();
                advertisement.MediaURL = await photoservice.UploadPhotoAsync(photo);
                var adBeacon = _context.Beacons.SingleOrDefault(b => b.Id == advertisement.BeaconId);
                advertisement.CompanyID = adBeacon.BussinessId;
                advertisement.UploadedDate = DateTime.UtcNow;
                _context.Advertisements.Add(advertisement);

            }
            else
            {
                var adInDb = _context.Advertisements.SingleOrDefault(a => a.Id == advertisement.Id);
                adInDb.BeaconId = advertisement.BeaconId;
                adInDb.Description = advertisement.Description;
                adInDb.ExpirationDate = advertisement.ExpirationDate;
                if (photo != null)
                {
                    PhotoService photoservice = new PhotoService();
                    adInDb.MediaURL = await photoservice.UploadPhotoAsync(photo);
                }
               
               
                adInDb.Title = advertisement.Title;
                adInDb.UploadedDate = DateTime.UtcNow;

            }
            await _context.SaveChangesAsync();

            var adInDB = _context.Advertisements.ToList().LastOrDefault();

            

            if (adInDB!= null&& advertisement.Id==0)
            {
                var fbAd = new FBAd();


                fbAd.category = _context.Beacons.SingleOrDefault(b => b.Id == adInDB.BeaconId).AreaId;
                fbAd.description = adInDB.Description;
                fbAd.id = adInDB.Id;
                fbAd.imageURL = adInDB.MediaURL;
                fbAd.publishedDate = adInDB.UploadedDate;
                fbAd.title = adInDB.Title;
               
                




                SetResponse response = await FBclient.SetAsync("Ads_test/" + adInDB.Id, fbAd);
            }

            return RedirectToAction("Index", "Advertisements");
        }




        public ActionResult Edit(int id)
        {
            var advertisement = _context.Advertisements.SingleOrDefault(c => c.Id == id);
            var beacons = _context.Beacons.ToList();
            if (advertisement == null)
                return HttpNotFound();

            var viewModel = new AdFormViewModel()
            {
                Advertisement = advertisement,
                Beacons = beacons
            
            };

            return PartialView("_AdvertisementsFormPartial", viewModel);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = _context.Advertisements.Find(id);

            if (advertisement == null)
            {
                return HttpNotFound();
            }

            _context.Advertisements.Remove(advertisement);
            _context.SaveChanges();
            FirebaseResponse response = await FBclient.DeleteAsync("Ads_test/" + id);
            

            return RedirectToAction("Index");


        }


    }
}