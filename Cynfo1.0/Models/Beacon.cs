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
        [Display(Name = "Direccion MAC")]
        public string MACAddress { get; set; }

        public int BussinessId { get; set; }

        public string BussinessName { get; set; }

        [Required]
        [Display(Name = "ID de Area")]
        [CheckAvailableAreas]
        public int? AreaId { get; set; }

        [Required]
        [Display(Name = "Nombre de Area")]
        public string AreaName { get; set; }

        public string AreaMediaUrl { get; set; }

    }
}