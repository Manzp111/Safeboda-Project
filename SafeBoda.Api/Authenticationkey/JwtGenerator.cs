using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SafeBoda.Core;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoda.Authenticationkey
{
    public class JwtGenerator
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;

        public JwtGenerator(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        /// <summary>
        /// Generates an access token and refresh token for a user.
        /// Includes role claims for role-based authorization.
        /// </summary>
        public async Task<(string AccessToken, RefreshToken RefreshToken)> GenerateTokensAsync(UserResponseDto user, string ipAddress)
        {
            var jwtSettings = _config.GetSection("JwtSettings");

            // Fetch roles for the user
            var appUser = await _userManager.FindByIdAsync(user.Id);
            var roles = await _userManager.GetRolesAsync(appUser);

            // Create claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("UserId", user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Add role claims
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // Generate signing credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"])),
                signingCredentials: creds
            );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            // Generate refresh token
            var refreshToken = GenerateRefreshToken(ipAddress, jwtSettings["RefreshTokenExpiryDays"]);

            return (accessToken, refreshToken);
        }

        /// <summary>
        /// Generates a secure refresh token.
        /// </summary>
        private RefreshToken GenerateRefreshToken(string ipAddress, string refreshExpiryDays)
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(double.Parse(refreshExpiryDays)),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }
    }
}
