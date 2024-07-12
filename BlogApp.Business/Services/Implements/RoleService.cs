using AutoMapper;
using BlogApp.Business.Dtos.RoleDtos;
using BlogApp.Business.Dtos.UserDtos;
using BlogApp.Business.Exceptions.AppUser;
using BlogApp.Business.Exceptions.Common;
using BlogApp.Business.Exceptions.Role;
using BlogApp.Business.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.DAL.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        readonly AppDbContext _context;

        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IMapper mapper, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _context = context;
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

        public async Task<ICollection<RoleDetailDto>> GetAllAsync()
        {
            ICollection<RoleDetailDto> users = new List<RoleDetailDto>();
            foreach (var role in await _roleManager.Roles.ToListAsync())
            {
                var existUser = await _userManager.GetUsersInRoleAsync(role.Name);
                users.Add(new RoleDetailDto
                {
                    Roles = _mapper.Map<RoleDto>(role),
                    Users = _mapper.Map<IEnumerable<AuthorDto>>(existUser)
                });
            }
            return users;
        }

        public async Task<string> GetByIdAsync(string id)
        {
            if (id is null) throw new ArgumentNullException();
            if(!await _roleManager.Roles.AnyAsync(x=>x.Id == id)) throw new RoleIdException();
            var name = (await _roleManager.FindByIdAsync(id)).Name ?? throw new NotFoundException<IdentityRole>();
            var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
            //var query = _roleManager.Roles.AsQueryable();
            //query.Include("AppUser").Include("AppUser.Name");
            //role = await query.FirstOrDefaultAsync();
            return role.Name;
        }

        public async Task RemoveAsync(string roleName)
        {
            if (roleName is null) throw new ArgumentNullException();
            var role = await _roleManager.FindByNameAsync(roleName) ?? throw new RoleExistException();
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

    }
}
