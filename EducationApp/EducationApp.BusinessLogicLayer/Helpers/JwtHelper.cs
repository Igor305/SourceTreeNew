using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Helpers
{
    public class JwtHelper : IJwtPrivateKey
    {
        private readonly SymmetricSecurityKey _accesstoken;
        private readonly IConfiguration _configuration;
        public string SigningAlgorithm { get; } = SecurityAlgorithms.HmacSha256;

        public JwtHelper(string keyaccesstoken, IConfiguration configuration)
        {
            _configuration = configuration;
            this._accesstoken = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyaccesstoken));
        }
        public JwtHelper()
        {

        }
        public SecurityKey GetKey() => this._accesstoken;

        public string GenerateToken(List<Claim> claims, int lifetime)
        {
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration.GetSection("JWT")["Issuer"],
                audience: _configuration.GetSection("JWT")["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(lifetime),
                signingCredentials: new SigningCredentials(
                        GetKey(),
                        SigningAlgorithm)
            );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
    }
}
