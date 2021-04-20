using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Threading.Tasks;
using ThePortal.Models;
using ThePortal.Models.Authentication;
using ThePortal.Models.LoginAndRegister;
using ThePortal.Services;

namespace ThePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeTokenController : ControllerBase
    {
        private readonly IAuthenticationService auth;
        private readonly ApplicationUserManager userManager;

        public ExchangeTokenController(IAuthenticationService auth, ApplicationUserManager userManager)
        {
            this.auth = auth;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> GetNewAccessToken(ExchangeTokenDto exchangeTokenDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthenticationResponse()
                {
                    Success = false,
                    Error = "Invalid request body"
                });
            }
            try
            {
                var principals = auth.GetClaimsFromExpiredToken(exchangeTokenDto.AccessToken);
                var userId = principals.Claims.SingleOrDefault(x => x.Type == nameof(ApplicationUser.Id)).Value;
                var user = await userManager.FindByIdAsync(userId);
                var storedRefreshtoken = user.RefreshToken;

                if (storedRefreshtoken.Token != exchangeTokenDto.RefreshToken)
                {
                    return BadRequest(new AuthenticationResponse()
                    {
                        Success = false,
                        Error = "Invalid refresh token"
                    });
                }
                if (storedRefreshtoken.ExpiryDate > DateTime.UtcNow)
                {
                    return BadRequest(new AuthenticationResponse()
                    {
                        Success = false,
                        Error = "Expired RefreshToken!"
                    });
                }
                var newAccessToken = auth.GenerateAccessToken(user);
                return Ok(new AuthenticationResponse()
                {
                    Success = true,
                    AccessToken = newAccessToken
                });
            }
            catch (Exception ex)
            {
                var error = "invalid operation";
                if (ex is SecurityTokenException)
                {
                    error = "invalid Access Token";
                }
                return BadRequest(new AuthenticationResponse()
                {
                    Success = false,
                    Error = error
                });
            }
        }
    }
}