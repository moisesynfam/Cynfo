using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
namespace Cynfo1._0.Models
{
    public class CheckAvailableAreas : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _context = new ApplicationDbContext();
            var userManager = _context.Users;
            string id = HttpContext.Current.User.Identity.GetUserId();
            var activeUser = userManager.SingleOrDefault(u => u.Id == id);

            var beacons = from b in _context.Beacons
                          where b.BussinessId.Equals(activeUser.CompanyIdentifier)
                          select b;
            var beacon = (Beacon) validationContext.ObjectInstance;
            if (beacon.Id == 0)
            {
                foreach (var b in beacons)
                {
                    if (b.AreaId == beacon.AreaId)
                    {
                        return new ValidationResult("Este ID ya esta siendo usado por otra area");
                    }
                }
            }
            return ValidationResult.Success;

            
        }
    }
}