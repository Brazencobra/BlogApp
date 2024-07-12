using BlogApp.Business.Dtos.UserDtos;
using BlogApp.Business.HelperServices.Interfaces;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
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
        readonly IRoleService _roleService;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly UserManager<AppUser> _userManager;

        public TokenHandler(IConfiguration configuration, IRoleService roleService, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _roleService = roleService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public TokenResponseDto CreateToken(AppUser user, int expires = 300)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.GivenName,user.UserName),
                new Claim(ClaimTypes.Surname,user.Surname),
                new Claim(ClaimTypes.Email,user.Email)
            };
            
            foreach (var item in _userManager.GetRolesAsync(user).Result)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurity = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(expires),
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
