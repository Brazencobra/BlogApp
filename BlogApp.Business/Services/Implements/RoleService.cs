using AutoMapper;
using BlogApp.Business.Exceptions.AppUser;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.Exceptions.Role;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.DAL.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Services.Implements
{
    public class RoleService : IRoleService
    {
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly IMapper _mapper;

        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task CreateAsync(string name)
        {
            if (await _roleManager.RoleExistsAsync(name)) throw new RoleExistException();
            var result = await _roleManager.CreateAsync(new IdentityRole
            {
                Name = name
            });
            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    sb.Append(error.Description + " ");
                }
                throw new RoleCreateFailedException(sb.ToString());
            }
        }

        public async Task<IEnumerable<string>> GetAllAsync()
        {
            return await _roleManager.Roles.Select(x=>x.Name).ToListAsync();
        }
        public async Task<string> GetByIdAsync2(string id)
        {
            if (id is null) throw new ArgumentNullException();
            if (!await _roleManager.Roles.AnyAsync(x => x.Id == id)) throw new RoleIdException();
            return (await _roleManager.FindByIdAsync(id)).Name ?? throw new RoleExistException();
        }
        public async Task<RoleDetailDto> GetByIdAsync(string id)
        {
            if (id is null) throw new ArgumentNullException();
            if(!await _roleManager.Roles.AnyAsync(x=>x.Id == id)) throw new RoleIdException();
            var name = (await _roleManager.FindByIdAsync(id)).Name ?? throw new RoleExistException();
            var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<RoleDetailDto>(role);
        }

        public async Task RemoveAsync(string id)
        {
            if (id is null) throw new ArgumentNullException();
            var role = await _roleManager.FindByIdAsync(id) ?? throw new RoleExistException();
            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    sb.Append(error.Description + " ");
                }
                throw new RoleRemoveException(sb.ToString());
            }
        }

        public async Task UpdateAsync(string id, string name)
        {
            if (id is null) throw new ArgumentNullException();
            var role = await _roleManager.FindByIdAsync(id) ?? throw new RoleExistException();
            role.Name = name;
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                var sb = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    sb.Append(error.Description + " ");
                }
                throw new RoleRemoveException(sb.ToString());
            }
        }
        public async Task GiveRoleAsync(string userId, string roleId)
        {
            if(userId is null || roleId is null) throw new ArgumentNullException();
            var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.Id == userId) ?? throw new UserNotFoundException();
            var role = (await _roleManager.FindByIdAsync(roleId)).Name ?? throw new RoleIdException();
            await _userManager.AddToRoleAsync(user, role);
        }
        public async Task TakeRoleAsync(string userId, string roleId)
        {
            if (userId is null || roleId is null) throw new ArgumentNullException();
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId) ?? throw new UserNotFoundException();
            var role = (await _roleManager.FindByIdAsync(roleId)).Name ?? throw new RoleIdException();
            await _userManager.RemoveFromRoleAsync(user, role);
        }
    }
}
