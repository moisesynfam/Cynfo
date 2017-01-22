using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using AutoMapper;
using Cynfo1._0.Dtos;
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
        public IEnumerable<AdvertisementDto> GetAdvertisements()
        {

            return _context.Advertisements.ToList().
                Select(Mapper.Map<Advertisement,AdvertisementDto>);
        }

        //GET /api/customers/1
        public AdvertisementDto GetAdvertisement(int id)
        {
            var advertisement = _context.Advertisements.SingleOrDefault(a => a.Id == id);

            if (advertisement == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Advertisement,AdvertisementDto>(advertisement);




        }

        // POST /api/customers
        [HttpPost]
        public AdvertisementDto CreateAdvertisement(AdvertisementDto advertisementDto)
        {

            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var advertisement = Mapper.Map<AdvertisementDto, Advertisement>(advertisementDto);

            _context.Advertisements.Add(advertisement);
            _context.SaveChanges();

            advertisementDto.Id = advertisement.Id;
            return advertisementDto;


        }

        //PUT /api/customers/1

        public void UpdateAdvertisement(int id, AdvertisementDto advertisementDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            

            var adInDB = _context.Advertisements.SingleOrDefault(a => a.Id == id);

            if (adInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(advertisementDto, adInDB);

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
