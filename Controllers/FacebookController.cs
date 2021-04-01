using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ThePortal.Models;
using ThePortal.Models.Facebook;
using ThePortal.Services.FacebookService;

namespace ThePortal.Controllers

{
    
   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FacebookController : ControllerBase
    {
        private readonly IFacebookService _facebookService;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationDbContext _db ;
        public FacebookController(IFacebookService facebookService,ApplicationUserManager userManager,ApplicationDbContext db)
        {
            _facebookService = facebookService;
            _userManager = userManager;
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            ApplicationUser user = await _userManager.FindByIdAsync(User.FindFirstValue("Id"));
            var adAccounts = await _facebookService.GetAllAdAccounts(user.FacebookData.AccessToken);
            var ads = await _facebookService.GetAllAds(adAccounts[0].Id, user.FacebookData.AccessToken);

            return Ok(ads);
        }
        
        [HttpGet]
        [Route("AdAccounts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FacebookAccount[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> AdAccounts()
        {
            try
            {
                var user = await _userManager.FindByIdAsync(User.FindFirstValue("Id"));
                return Ok(await _facebookService.GetAllAdAccounts(user.FacebookData.AccessToken));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(FacebookAccessTokenResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ExchangeAcessToken([FromBody] ExchangeAcessTokenRequestModel token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request body");
            }

            try
            {

                ApplicationUser user = await _userManager.FindByIdAsync(User.FindFirstValue("Id"));
                var longAccessTokenResponse = await _facebookService.GetLonglivedAccessToken(token.AccessToken);
                var facebookdata = await _userManager.UpdateFacebookAccessTokenAsync(user.Id, longAccessTokenResponse.AccessToken);

                return Ok(facebookdata.AccessToken);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
            
        }
    }
}
