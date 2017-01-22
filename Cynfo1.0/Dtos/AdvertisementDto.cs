using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Cynfo1._0.Models;

namespace Cynfo1._0.Dtos
{
    public class AdvertisementDto
    {

        public int Id { get; set; }

        [Required]
        [StringLength(50)]

        public string Title { get; set; }

        [StringLength(255)]

        public string Description { get; set; }

        public DateTime UploadedDate { get; set; }


        public string MediaURL { get; set; }

        [Required]
        public int BeaconId { get; set; }

       
    }
}