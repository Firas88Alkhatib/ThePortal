using Microsoft.AspNetCore.Http;
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
        private readonly ApplicationUserManager _userManager;

        public LoginController(IAuthenticationService auth, ApplicationUserManager userManager)
        {
            _auth = auth;
            _userManager = userManager;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(AuthenticationResponse))]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthenticationResponse()
                {
                    Success = false,
                    Error = "Invalid request"
                });
            }

            ApplicationUser user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return BadRequest(new AuthenticationResponse()
                {
                    Success = false,
                    Error = "user not found",
                });
            }
            bool passwordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!passwordCorrect)
            {
                return BadRequest(new AuthenticationResponse()
                {
                    Success = false,
                    Error = "Incorrect password",
                });
            }
            var accessToken = _auth.GenerateAccessToken(user);
            return Ok(new AuthenticationResponse()
            {
                Success = true,
                AccessToken = accessToken
            });
        }
    }
}