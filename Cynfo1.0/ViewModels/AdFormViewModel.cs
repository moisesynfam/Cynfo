using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cynfo1._0.Models;

namespace Cynfo1._0.ViewModels
{
    public class AdFormViewModel
    {
        public IEnumerable<Beacon> Beacons { get; set; }
        public Advertisement Advertisement { get; set; }
    }
}