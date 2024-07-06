using AutoMapper;
using BlogApp.Business.Dtos.UserDtos;
using BlogApp.Business.Exceptions.AppUser;
using BlogApp.Business.HelperServices.Interfaces;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Services.Implements
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _user;
        readonly IMapper _mapper;
        readonly ITokenHandler _tokenHandler;
        public UserService(UserManager<AppUser> user, IMapper mapper, ITokenHandler tokenHandler)
        {
            _user = user;
            _mapper = mapper;
            _tokenHandler = tokenHandler;
        }

        public async Task<TokenResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _user.FindByNameAsync(dto.UserName);
            if (user == null) throw new LoginFailedException("İstifadəçi adı və ya şifrə yanlışdır");
            var result = await _user.CheckPasswordAsync(user, dto.Password);
            if (!result) throw new LoginFailedException("İstifadəçi adı və ya şifrə yanlışdır");
            return _tokenHandler.CreateToken(user);
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            var user = _mapper.Map<AppUser>(dto);
            if (await _user.Users.AnyAsync(x=>x.UserName == dto.UserName || x.Email == dto.Email))
            {
                throw new UserExistException();
            }
            var result = await _user.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in result.Errors)
                {
                    sb.Append(item.Description + " ");
                }
                throw new RegisterFailedException(sb.ToString().TrimEnd());
            }
        }
    }
}
