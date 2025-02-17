﻿using KUSYS_Demo_Web_Uygulamasi.Services.IServices;
using Microsoft.IdentityModel.Tokens;
using static System.Net.Mime.MediaTypeNames;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using KUSYS_Demo_Web_Uygulamasi.Models.Entities.Identity;
using KUSYS_Demo_Web_Uygulamasi.DTOs;

namespace KUSYS_Demo_Web_Uygulamasi.Services
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenDTO CreateAccessToken(int second,AppUser appUser)
        {
            TokenDTO token = new();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            SigningCredentials siginigCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            token.Expiration = DateTime.UtcNow.AddSeconds(second);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: siginigCredentials,
                claims:new List<Claim> { new(ClaimTypes.Name,appUser.UserName)}
                );
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);

            return token;

        }


 
    }
}
