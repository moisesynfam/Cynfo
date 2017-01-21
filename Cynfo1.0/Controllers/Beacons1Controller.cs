using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cynfo1._0.Models;

namespace Cynfo1._0.Controllers
{
    public class Beacons1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Beacons1
        public ActionResult Index()
        {
            return View(db.Beacons.ToList());
        }

        // GET: Beacons1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beacon beacon = db.Beacons.Find(id);
            if (beacon == null)
            {
                return HttpNotFound();
            }
            return View(beacon);
        }

        // GET: Beacons1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Beacons1/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MACAddress,BussinessId,BussinessName,AreaId,AreaName,AreaMediaUrl")] Beacon beacon)
        {
            if (ModelState.IsValid)
            {
                db.Beacons.Add(beacon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(beacon);
        }

        // GET: Beacons1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beacon beacon = db.Beacons.Find(id);
            if (beacon == null)
            {
                return HttpNotFound();
            }
            return View(beacon);
        }

        // POST: Beacons1/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MACAddress,BussinessId,BussinessName,AreaId,AreaName,AreaMediaUrl")] Beacon beacon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beacon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(beacon);
        }

        // GET: Beacons1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beacon beacon = db.Beacons.Find(id);
            if (beacon == null)
            {
                return HttpNotFound();
            }
            return View(beacon);
        }

        // POST: Beacons1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Beacon beacon = db.Beacons.Find(id);
            db.Beacons.Remove(beacon);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
