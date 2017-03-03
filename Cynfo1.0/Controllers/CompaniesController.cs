using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cynfo1._0.Models;

namespace Cynfo1._0.Controllers
{
    public class CompaniesController : Controller

    {

        private ApplicationDbContext _context;

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Companies
        public ActionResult Index()
        {
            var users = _context.Users.ToList();


            return View(users);
        }
    }
}