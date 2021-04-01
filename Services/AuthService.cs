using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ThePortal.Configuration;
using ThePortal.Models;
using ThePortal.Models.Authentication;

namespace ThePortal.Services
{
    public interface IAuthenticationService
    {
        string GenerateAccessToken(ApplicationUser user);
        Task<UserCreationResult> CreateNewUser(UserRegisterationModel user);
        Task<string> Login(LoginModel loginModel);
    }

    public class AuthService : IAuthenticationService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly ApplicationUserManager _userManager;

        public AuthService(JwtConfig jwtConfig, ApplicationUserManager userManager)
        {
            _jwtConfig = jwtConfig;
            _userManager = userManager;
        }

        public string GenerateAccessToken(ApplicationUser user)
        {
            var jwtSecterKey = Encoding.ASCII.GetBytes(_jwtConfig.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtSecterKey), SecurityAlgorithms.HmacSha512Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var accessToken = jwtTokenHandler.WriteToken(token);
            return accessToken;
        }
        public async Task<UserCreationResult> CreateNewUser(UserRegisterationModel user)
        {
            ApplicationUser existedUser = await _userManager.FindByEmailAsync(user.Email);
            if (existedUser != null)
            {
                return new UserCreationResult()
                {
                    Success = false,
                    Error = "user with this email already exist"
                };
            }
            ApplicationUser newUser = new() { Email = user.Email, UserName = user.Name , FacebookData = user.FacebookData ?? new FacebookData()};
            IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
            if (!result.Succeeded)
            {
                return new UserCreationResult() { 
                Success = false,
                Error = string.Join(",", result.Errors.Select(c => c.Description).ToList())
                };
            }
            return new UserCreationResult() {Success= true,User = newUser };
            
        }
        public async Task<string> Login(LoginModel loginModel)
        {
            // TODO: unique email
            ApplicationUser user = await _userManager.FindByEmailAsync(loginModel.Email);
            if(user == null)
            {
                throw new Exception("User not found");
            }
            bool passwordCorrect = await _userManager.CheckPasswordAsync(user, loginModel.Password);
            if (!passwordCorrect)
            {
                throw new Exception("Wrong password");
            }
            return GenerateAccessToken(user);
        }

    }
}
