using AutoMapper;
using BlogApp.Business.Dtos.UserDtos;
using BlogApp.Business.Exceptions.AppUser;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Services.Implements
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _user;

        readonly IMapper _mapper;
        public UserService(UserManager<AppUser> user,IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
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
