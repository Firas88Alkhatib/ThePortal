using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThePortal.Models.Authentication
{
    public class UserCreationResult
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public ApplicationUser User { get; set; }
    }
}
