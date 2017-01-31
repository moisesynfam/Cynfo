using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using AutoMapper;
using Cynfo1._0.Dtos;
using Cynfo1._0.Models;

namespace Cynfo1._0.Controllers.Api
{
    public class BeaconsController : ApiController
    {
        private ApplicationDbContext _context;

        public BeaconsController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/Beacons

        public IHttpActionResult GetBeacons()
        {
            var beaconDtos = _context.Beacons
                .ToList()
                .Select(Mapper.Map<Beacon, BeaconDto>);

            return Ok(beaconDtos);

        }

        //GET /api/beacons/1
        public IHttpActionResult GetBeacon(int id)
        {
            var beacon = _context.Beacons.SingleOrDefault(b => b.Id == id);
            if (beacon == null)
                return NotFound();

            return Ok(Mapper.Map<Beacon, BeaconDto>(beacon));

        }

        //POST /api/beacons

        [HttpPost]
        public IHttpActionResult CreateBeacon(BeaconDto beaconDto)
        {

            if (!ModelState.IsValid)
                return BadRequest();
            var beacon = Mapper.Map<BeaconDto, Beacon>(beaconDto);
            _context.Beacons.Add(beacon);
            _context.SaveChanges();

            beaconDto.Id = beaconDto.Id;

            return Created(new Uri(Request.RequestUri + "/" + beacon.Id), beaconDto);

        }

        //PUT /api/beacons/1
        [HttpPut]
        public IHttpActionResult UpdateBeacon(int id, BeaconDto beaconDto)
        {

            if (!ModelState.IsValid) 
                return BadRequest();

            var beaconInDb = _context.Beacons.SingleOrDefault(a => a.Id == id);

            if (beaconInDb == null)
                return NotFound();

            Mapper.Map(beaconDto, beaconInDb);
            _context.SaveChanges();

            return Ok();


        }
        //DELETE /api/beacons/1
        [HttpDelete]
        public IHttpActionResult DeleteBeacon(int id)
        {
            var beaconInDb = _context.Beacons.SingleOrDefault(a => a.Id == id);
            if(beaconInDb == null) 
                return NotFound();

            _context.Beacons.Remove(beaconInDb);
            _context.SaveChanges();

            return Ok();

        }



    }

}
