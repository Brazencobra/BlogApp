using BlogApp.Business.Dtos.UserDtos;
using BlogApp.Business.HelperServices.Interfaces;
using BlogApp.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.HelperServices.Implements
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenResponseDto CreateToken(AppUser user, int expires = 60)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.GivenName,user.UserName),
                new Claim(ClaimTypes.Surname,user.Surname),
                new Claim(ClaimTypes.Email,user.Email)
            };
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurity = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(60),
                credentials);
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
            string token = jwtHandler.WriteToken(jwtSecurity);
            return new()
            {
                Token = token,
                Expires = jwtSecurity.ValidTo,
                UserName = user.UserName
            };
        }
    }
}
