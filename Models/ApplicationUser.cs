using Microsoft.AspNetCore.Identity;
using ThePortal.Models.Google;

namespace ThePortal.Models
{
    public class ApplicationUser : IdentityUser
    {
        public FacebookData FacebookData { get; set; }
        public GoogleData GoogleData { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public string BusinessName { get; set; }
    }
}