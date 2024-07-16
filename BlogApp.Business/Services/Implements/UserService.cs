using AutoMapper;
using BlogApp.Business.Dtos.UserDtos;
using BlogApp.Business.Exceptions.AppUser;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.Exceptions.Role;
using BlogApp.Business.HelperServices.Interfaces;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
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
        readonly RoleManager<IdentityRole> _roleManager;
        readonly IHttpContextAccessor _contextAccessor;
        readonly string _userId;
        readonly string _authorizationToken;
        public UserService(UserManager<AppUser> user, IMapper mapper, ITokenHandler tokenHandler, RoleManager<IdentityRole> roleManager, IHttpContextAccessor contextAccessor)
        {
            _user = user;
            _mapper = mapper;
            _tokenHandler = tokenHandler;
            _roleManager = roleManager;
            _contextAccessor = contextAccessor;
            _userId = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _authorizationToken = _contextAccessor?.HttpContext?.Request?.Headers?["Authorization"].ToString();
        }

        public async Task<ICollection<AppUserListItemDto>> GetAllAsync()
        {
            ICollection<AppUserListItemDto> users = new List<AppUserListItemDto>();
            foreach (var user in await _user.Users.ToListAsync())
            {
                users.Add(new AppUserListItemDto
                {   
                    Users = _mapper.Map<AuthorDto>(user),
                    Roles = await _user.GetRolesAsync(user)
                });
            }
            return users;
        }
        //public async Task<IEnumerable<AppUserListItemDto>> GetAllAsync()
        //{
        //    var users =  _user.Users.AsQueryable();
        //    return _mapper.Map<IEnumerable<AppUserListItemDto>>(users);
        //}

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

        public async Task GiveRoleAsync(string userName, string roleName)
        {
            if (userName is null || roleName is null) throw new ArgumentNullException();
            var user = await _user.Users.FirstOrDefaultAsync(x => x.UserName == userName) ?? throw new UserNotFoundException();
            if(!await _roleManager.RoleExistsAsync(roleName)) throw new NotFoundException<IdentityRole>("Bele bir role movcud deyil");
            var role = (await _roleManager.FindByNameAsync(roleName)).Name;
            
            var result = await _user.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in result.Errors)
                {
                    sb.Append(item.Description + " ");
                }
                throw new RoleCreateFailedException(sb.ToString());
            }
        }

        public async Task TakeRoleAsync(string userName, string roleName)
        {
            if (userName is null || roleName is null) throw new ArgumentNullException();
            var user = await _user.Users.FirstOrDefaultAsync(x => x.UserName == userName) ?? throw new UserNotFoundException();
            var role = (await _roleManager.FindByNameAsync(roleName)).Name ?? throw new RoleIdException();
            var result = await _user.RemoveFromRoleAsync(user, role);
            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in result.Errors)
                {
                    sb.Append(item.Description + " ");
                }
                throw new RoleCreateFailedException(sb.ToString());
            }
        }

        public async Task<TokenResponseDto> LoginWithRefreshTokenAsync(string refreshToken)
        {
            if(string.IsNullOrEmpty(refreshToken)) throw new NegativeIdException();
            var user = await _user.Users.SingleOrDefaultAsync(x => x.RefreshToken == refreshToken);
            if (user is null) throw new NotFoundException<AppUser>(); 
            if(user.RefreshTokenExpiresDate < DateTime.UtcNow) throw new RefreshTokenExpiredException();
            return _tokenHandler.CreateToken(user);
        }

        public async Task ResetPasswordAsync(string currentPassword,string newPassword)
        {
            var user = await _user.Users.FirstOrDefaultAsync(x => x.Id == _userId);
            if (user is null) throw new NotFoundException<AppUser>();
            var result = await _user.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in result.Errors)
                {
                    sb.Append(item.Description);
                }
                throw new Exception(sb.ToString());
            }
        }

        public async Task<AppUserDetailDto> GetUserInformation()
        {
            var user = await _user.Users.FirstOrDefaultAsync(x=>x.Id == _userId);
            if (user is null) throw new NotFoundException<AppUser>();
            return _mapper.Map<AppUserDetailDto>(user);
        }
    }
}
