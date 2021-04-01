
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThePortal.Configuration;
using ThePortal.Models;
using ThePortal.Models.Authentication;
using ThePortal.Services;

namespace ThePortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
       
        private readonly IAuthenticationService _authService;


        public RegisterController(IAuthenticationService authService)
        {
            _authService = authService;

        }

        [HttpPost]
        public async Task<IActionResult> RegisterNewUser([FromBody] UserRegisterationModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthenticationResponse()
                {
                    Success = false,
                    Error = "Invalid request"
                });
            }
            UserCreationResult creationResult = await _authService.CreateNewUser(user);
            if (!creationResult.Success)
            {
                return BadRequest(new AuthenticationResponse() { 
                Success = false,
                Error = creationResult.Error
                });
            }

            string accessToken = _authService.GenerateAccessToken(creationResult.User);

            return Ok(new AuthenticationResponse() {
            Success = true,
            AccessToken = accessToken
            });
        }
    }
}
