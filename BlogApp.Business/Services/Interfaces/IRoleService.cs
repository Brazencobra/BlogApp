using BlogApp.Business.Dtos.CategoryDtos;
using BlogApp.Business.Dtos.RoleDtos;
using BlogApp.Business.Services.Implements;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Services.Interfaces
{
    public interface IRoleService
    {
        Task<ICollection<RoleDetailDto>> GetAllAsync();
        Task<string> GetByIdAsync(string id);
        //Task<string> GetByIdAsync(string id);
        Task CreateAsync(string name);
        Task UpdateAsync(string id, string name);
        Task RemoveAsync(string roleName);
    }
}
