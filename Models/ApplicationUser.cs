using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ThePortal.Models.Google;

namespace ThePortal.Models
{
    public class ApplicationUser : IdentityUser
    {
        public FacebookData FacebookData { get; set; }
        public GoogleData GoogleData { get; set; }
        public string BusinessName { get; set; }

    }
}
