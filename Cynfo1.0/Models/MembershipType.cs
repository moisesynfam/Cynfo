using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cynfo1._0.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public int AdsCount { get; set; }
        public int? Price { get; set; }

    }
}