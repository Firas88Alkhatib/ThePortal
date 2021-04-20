using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ThePortal.Configuration;
using ThePortal.Models;

namespace ThePortal.Services
{
    public interface IAuthenticationService
    {
        public string GenerateAccessToken(ApplicationUser user);

        public RefreshToken GenerateRefreshToken(string token);

        public ClaimsPrincipal GetClaimsFromExpiredToken(string token);
    }

    public class AuthService : IAuthenticationService
    {
        private readonly JwtConfig _jwtConfig;

        public AuthService(JwtConfig jwtConfig)
        {
            _jwtConfig = jwtConfig;
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

        public RefreshToken GenerateRefreshToken(string token)
        {
            var principal = GetClaimsFromExpiredToken(token);
            var jti = principal.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var userId = principal.Claims.SingleOrDefault(x => x.Type == nameof(ApplicationUser.Id)).Value;
            var refreshToken = new RefreshToken()
            {
                JwtId = jti,
                UserId = userId,
                CreateDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(_jwtConfig.RefreshTokenExpiryInMonths),
                Token = GenerateRandomString() + Guid.NewGuid()
            };
            return refreshToken;
        }

        public ClaimsPrincipal GetClaimsFromExpiredToken(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals("HS512", StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        public string GenerateRandomString(int length = 32)
        {
            var random = new byte[length];
            using (var randomGenerator = RandomNumberGenerator.Create())
            {
                randomGenerator.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
    }
}