using API.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services.Jwtservices
{
    public class JwtService : IJwtService
    {
        #region Members
        private readonly JwtSettings jwtSettings;
        #endregion
        #region Constructor
        public JwtService(IOptions<JwtSettings> jwtsettings)
        {
            this.jwtSettings = jwtsettings.Value;
        }
        #endregion
        #region Methods
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
           {
               new Claim("userId",user.Id.ToString()),
               new Claim("email",user.Email),
               new Claim("username",user.Username),
               new Claim("role",user.Role.ToString())
           };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpirationMinutes),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
