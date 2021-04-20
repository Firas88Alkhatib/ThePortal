using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using ThePortal.Models;
using ThePortal.Models.Authentication;
using ThePortal.Models.Google;
using ThePortal.Services;

namespace ThePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly ApplicationUserManager _userManager;

        public RegisterController(IAuthenticationService authService, ApplicationUserManager userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(AuthenticationResponse))]
        public async Task<IActionResult> RegisterNewUser([FromBody] UserRegisterDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthenticationResponse()
                {
                    Success = false,
                    Error = "Invalid request"
                });
            }

            ApplicationUser existedUser = await _userManager.FindByEmailAsync(user.Email);
            if (existedUser != null)
            {
                return BadRequest(new AuthenticationResponse()
                {
                    Success = false,
                    Error = "user already exists!"
                });
            }
            ApplicationUser newUser = new() {
                Email = user.Email,
                UserName = user.Name,
                FacebookData = user.FacebookData ?? new FacebookData(),
                GoogleData = user.GoogleData ?? new GoogleData()
            };
            IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new AuthenticationResponse()
                {
                    Success = false,
                    Error = string.Join(",", result.Errors.Select(c => c.Description).ToList())
                });
            }

            string accessToken = _authService.GenerateAccessToken(newUser);

            return Ok(new AuthenticationResponse()
            {
                Success = true,
                AccessToken = accessToken
            });
        }
    }
}