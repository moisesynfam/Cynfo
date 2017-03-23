using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Cynfo1._0.App_Start;
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


        public AdvertisementsController()
        {
            
            _context = new ApplicationDbContext();
          


        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        private ApplicationUser GetActiveUser()
        {

            var userManager = _context.Users;
            string activeUserId = User.Identity.GetUserId();
            var activeUser = userManager.SingleOrDefault(u => u.Id == activeUserId);

            return activeUser;

        }

        // GET: Advertisements
        public async Task<ActionResult> Index()
        {


            var activeUser = GetActiveUser();
            var advertisements = from a in _context.Advertisements
                where a.CompanyID.Equals(activeUser.CompanyIdentifier)
                select a;


            var beacons = from b in _context.Beacons
                          where b.BussinessId.Equals(activeUser.CompanyIdentifier)
                          select b;
           




            var adsViewModel = new AdsViewModel()
            {
                Advertisements = advertisements.ToList(),
                Beacons = beacons.ToList()
           
            };
            

            return View(adsViewModel);
        }

        public ActionResult AreaAds(int id)
        {

            var activeUser = GetActiveUser();
            var advertisements = from a in _context.Advertisements
                                 where a.CompanyID.Equals(activeUser.CompanyIdentifier)
                                 select a;


            var beacons = from b in _context.Beacons
                          where b.BussinessId.Equals(activeUser.CompanyIdentifier)
                          select b;


            var areaAds = from a in advertisements
                          where a.BeaconId.Equals(id)
                          select a;

            if (!areaAds.Any())
            {
                return HttpNotFound();
            }

            var adsViewModel = new AdsViewModel()
            {
                Advertisements = areaAds.ToList(),
                Beacons = beacons.ToList()

            };

            ViewBag.AreaAdsTitle = beacons.First().AreaName;
            return PartialView("_AreaAds", adsViewModel);
        }

        public ActionResult Create()
        {

            var activeUser = GetActiveUser();

            var beacons = from b in _context.Beacons
                          where b.BussinessId.Equals(activeUser.CompanyIdentifier)
                          select b;
            var viewModel = new AdFormViewModel()
            {
                Advertisement = new Advertisement(),
                Beacons = beacons.ToList()
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

            //var adInDB = _context.Advertisements.ToList().LastOrDefault();

            

                var fbAd = new FBAd();


                //fbAd.category = _context.Beacons.SingleOrDefault(b => b.Id == adInDB.BeaconId).AreaId;
                fbAd.description = advertisement.Description;
                fbAd.id = advertisement.Id;
                fbAd.imageURL = advertisement.MediaURL;
                //fbAd.publishedDate = adInDB.UploadedDate;
                fbAd.title = advertisement.Title;
               
                




                SetResponse response = await FirebaseInit.Firebaseclient.SetAsync("businessTest/" + advertisement.CompanyID+"/areas/"
                                                +"ar"+advertisement.CompanyID+advertisement.BeaconId+"/ads/"
                                                +"ad"+ advertisement.BeaconId+advertisement.Id, fbAd);
            

            return RedirectToAction("Index", "Advertisements");
        }




        public ActionResult Edit(int id)
        {
            var advertisement = _context.Advertisements.SingleOrDefault(c => c.Id == id);
            var activeUser = GetActiveUser();

            var beacons = from b in _context.Beacons
                          where b.BussinessId.Equals(activeUser.CompanyIdentifier)
                          select b;
            if (advertisement == null)
                return HttpNotFound();

            var viewModel = new AdFormViewModel()
            {
                Advertisement = advertisement,
                Beacons = beacons
            
            };

            return PartialView("_AdvertisementsFormPartial", viewModel);
        }

        public ActionResult PreDelete(int? id)
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

            return PartialView("_DeleteAd", advertisement);

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
           // FirebaseResponse response = await FBclient.DeleteAsync("Ads_test/" + id);
            

            return RedirectToAction("Index");


        }





    }
}