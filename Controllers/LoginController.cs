using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ThePortal.Models;
using ThePortal.Models.Authentication;
using ThePortal.Services;

namespace ThePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _auth;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginController(IAuthenticationService auth,UserManager<ApplicationUser> userManager)
        {
            _auth = auth;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<AuthenticationResponse> Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return new AuthenticationResponse()
                {
                    Success = false,
                    Error = "Invalid request"
                };
            }

            var accessToken = await _auth.Login(loginModel);
            return new AuthenticationResponse() {
                Success = true,
                AccessToken = accessToken
            };
        }
    }
}
