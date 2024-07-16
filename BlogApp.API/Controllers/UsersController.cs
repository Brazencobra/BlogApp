using BlogApp.Business.Services.Implements;
using BlogApp.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userService.GetAllAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> GiveRole(string userName, string roleName)
        {
            await _userService.GiveRoleAsync(userName, roleName);
            return Ok();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("[action]")]
        public async Task<IActionResult> TakeRole(string userName, string roleName)
        {
            await _userService.TakeRoleAsync(userName, roleName);
            return Ok();
        }
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetInformation()
        {
            return Ok(await _userService.GetUserInformation());
        }
    }
}
