using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Cynfo1._0.Dtos;
using Cynfo1._0.Models;

namespace Cynfo1._0.App_Start
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            Mapper.CreateMap<Advertisement, AdvertisementDto>();
            Mapper.CreateMap<AdvertisementDto, Advertisement>()
                .ForMember(a => a.Id, opt => opt.Ignore());
            Mapper.CreateMap<Beacon, BeaconDto>();
            Mapper.CreateMap<BeaconDto, Beacon>()
                .ForMember(b => b.Id, opt => opt.Ignore());
            ;

        }
    }
}