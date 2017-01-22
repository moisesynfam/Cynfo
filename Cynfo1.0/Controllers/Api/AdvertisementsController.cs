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
        public IHttpActionResult GetAdvertisements()
        {

            var advertisementDtos = _context.Advertisements.ToList().
                Select(Mapper.Map<Advertisement,AdvertisementDto>);

            return Ok(advertisementDtos);
        }

        //GET /api/customers/1
        public IHttpActionResult GetAdvertisement(int id)
        {
            var advertisement = _context.Advertisements.SingleOrDefault(a => a.Id == id);

            if (advertisement == null)
                return NotFound();

            return Ok(Mapper.Map<Advertisement,AdvertisementDto>(advertisement));




        }

        // POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateAdvertisement(AdvertisementDto advertisementDto)
        {

            if (!ModelState.IsValid)
                return BadRequest();
            
            var advertisement = Mapper.Map<AdvertisementDto, Advertisement>(advertisementDto);

            _context.Advertisements.Add(advertisement);
            _context.SaveChanges();

            advertisementDto.Id = advertisement.Id;
            return Created(new Uri(Request.RequestUri + "/"+ advertisement.Id), advertisementDto);


        }

        //PUT /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateAdvertisement(int id, AdvertisementDto advertisementDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            

            var adInDB = _context.Advertisements.SingleOrDefault(a => a.Id == id);

            if (adInDB == null)
                return NotFound();

            Mapper.Map(advertisementDto, adInDB);

            _context.SaveChanges();

            return Ok();

        }

        [HttpDelete]
        public IHttpActionResult DeleteAdvertisement(int id)
        {
            var adInDB = _context.Advertisements.SingleOrDefault(a => a.Id == id);

            if (adInDB == null)
                return NotFound();

            _context.Advertisements.Remove(adInDB);
            _context.SaveChanges();


            return Ok();

        }



    }
}
