using BlogApp.Business.Dtos.CategoryDtos;
using BlogApp.Business.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<string>> GetAllAsync();
        Task<RoleDetailDto> GetByIdAsync(string id);
        Task CreateAsync(string name);
        Task UpdateAsync(string id, string name);
        Task RemoveAsync(string id);
        Task GiveRoleAsync(string userId,string roleId);
        Task TakeRoleAsync(string userId,string roleId);
    }
}
