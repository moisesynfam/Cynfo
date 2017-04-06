using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cynfo1._0.Models
{
    public class Advertisement
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [StringLength(255)]
        [Display(Name="Descripción")]
        public string Description { get; set; }

        public DateTime UploadedDate { get; set; }

        [Display(Name = "Fecha de expiración")]
        public DateTime? ExpirationDate { get; set; }

        public Boolean isExpired { get; set; }

        public string MediaURL { get; set; }

        [Required]
        [Display(Name = "Area de publicacion")]
        public int BeaconId { get; set; }

        public Beacon Beacon { get; set; }

        public int CompanyID { get; set; }

        public TimeSpan GetPublishedTimeSpan()
        {
            TimeSpan timeSpan = new TimeSpan();
            if (ExpirationDate != null)
            {
                var timespan = ExpirationDate.Value - System.DateTime.Now;
                if (timespan.TotalMinutes <= 0)
                {
                    isExpired = true;
                }
                

            }
            return timeSpan;
        } 

    }
}