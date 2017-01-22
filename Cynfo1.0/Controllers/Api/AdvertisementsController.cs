using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using Cynfo1._0.Models;

namespace Cynfo1._0.Controllers.Api
{
    public class AdvertisementsController : ApiController
    {
        private ApplicationDbContext _context;

        public AdvertisementsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers
        public IEnumerable<Advertisement> GetAdvertisements()
        {
            return _context.Advertisements.ToList();
        }

        //GET /api/customers/1
        public Advertisement GetAdvertisement(int id)
        {
            var advertisement = _context.Advertisements.SingleOrDefault(a => a.Id == id);

            if (advertisement == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return advertisement;



        }

        // POST /api/customers
        [HttpPost]
        public Advertisement CreateAdvertisement(Advertisement advertisement)
        {

            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _context.Advertisements.Add(advertisement);
            _context.SaveChanges();
            return advertisement;


        }

        //PUT /api/customers/1

        public void UpdateAdvertisement(int id, Advertisement advertisement)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            

            var adInDB = _context.Advertisements.SingleOrDefault(a => a.Id == id);

            if (adInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            adInDB = advertisement;
            _context.SaveChanges();

        }

        [HttpDelete]
        public void DeleteAdvertisement(int id)
        {
            var adInDB = _context.Advertisements.SingleOrDefault(a => a.Id == id);

            if (adInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Advertisements.Remove(adInDB);
            _context.SaveChanges();




        }



    }
}
