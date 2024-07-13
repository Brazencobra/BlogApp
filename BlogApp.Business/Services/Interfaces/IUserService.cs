using BlogApp.Business.Dtos.UserDtos;
using BlogApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterDto dto);
        Task<TokenResponseDto> LoginAsync(LoginDto dto);
        Task<TokenResponseDto> LoginWithRefreshTokenAsync(string refreshToken);
        Task<ICollection<AppUserListItemDto>> GetAllAsync();
        Task GiveRoleAsync(string userName, string roleName);
        Task TakeRoleAsync(string userName, string roleName);
    }
}
