using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using SafeBoda.Core;
using System;
using System.Security.Cryptography;

namespace SafeBoda.Authenticationkey
{
    public class JwtGenerator
    {
        private readonly IConfiguration _config;

        public JwtGenerator(IConfiguration config)
        {
            _config = config;
        }

        public (string AccessToken, RefreshToken RefreshToken) GenerateTokens(UserResponseDto user, string ipAddress)
        {
            var jwtSettings = _config.GetSection("JwtSettings");

            // Access Token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim("UserId", user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                },
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"])),
                signingCredentials: creds
            );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            // Refresh Token
            var refreshToken = GenerateRefreshToken(ipAddress, jwtSettings["RefreshTokenExpiryDays"]);

            return (accessToken, refreshToken);
        }

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
