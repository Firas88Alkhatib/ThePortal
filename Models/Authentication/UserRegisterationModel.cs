using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ThePortal.Models.Google;

namespace ThePortal.Models.Authentication
{
    public class UserRegisterationModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public FacebookData FacebookData { get; set; }
        public GoogleData GoogleData { get; set; }

    }
}
