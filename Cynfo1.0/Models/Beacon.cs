using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cynfo1._0.Models
{
    public class Beacon
    {
        public int Id { get; set; }

        [Required]
        public string MACAddress { get; set; }

        public int BussinessId { get; set; }

        public string BussinessName { get; set; }

        [Required]
        public int AreaId { get; set; }

        [Required]
        public string AreaName { get; set; }

        public string AreaMediaUrl { get; set; }

    }
}