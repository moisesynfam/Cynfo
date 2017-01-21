using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cynfo1._0.Models;

namespace Cynfo1._0.ViewModels
{
    public class AdsViewModel
    {

        public IEnumerable<Beacon> Beacons { get; set; }
        public IEnumerable<Advertisement> Advertisements { get; set; }
    }
}