using BlogApp.Business.Dtos.UserDtos;
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
    }
}
